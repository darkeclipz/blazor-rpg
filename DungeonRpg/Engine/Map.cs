﻿using DungeonRpg.Services;
using Microsoft.AspNetCore.Razor.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Timers;

namespace DungeonRpg.Engine
{
    [JsonObject(MemberSerialization.Fields)]
    public class Map : IKey<Map>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        private int[,,] Data { get; set; }
        public const int Layers = 3;
        public enum LayerType { Floor = 0, Solid = 1, Overlay = 2 }
        private List<Entity> Entities { get; set; }
        private List<MapItem> Items { get; set; }
        private Timer AiUpdateTimer { get; set; }

        public Map()
        {
            Data = new int[Layers, Width, Height];
            Entities = new List<Entity>();
            Items = new List<MapItem>();
            StartAiTimer();
        }

        public void StartAiTimer()
        {
            AiUpdateTimer = new Timer();
            AiUpdateTimer.Elapsed += AiUpdateTimer_Elapsed;
            AiUpdateTimer.AutoReset = true;
            AiUpdateTimer.Interval = 1000;
            AiUpdateTimer.Start();
        }

        private void AiUpdateTimer_Elapsed(object sender, EventArgs args)
        {
            foreach(var entity in Entities)
            {
                if(entity is Enemy enemy)
                {
                    enemy.AiTick(this);
                }
            }
        }

        public void UpdateProperties()
        {
            //if(Entities == null)
            //{
            //    Entities = new List<MapEntity>();
            //}
            //if (Items == null)
            //{
            //    Items = new List<MapItem>();
            //}

            //Entities.Clear();
            //Items.Clear();
        }

        public IEnumerable<LayerType> LayersEnumerable() => Enumerable.Range(0, Layers).Select(i => (LayerType)i);

        private T[,,] ResizeArray<T>(T[,,] original, int x, int y, int z)
        {
            var newArray = new T[x, y, z];
            int minX = Math.Min(x, original.GetLength(0));
            int minY = Math.Min(y, original.GetLength(1));
            int minZ = Math.Min(z, original.GetLength(2));
            for (int i = 0; i < minX; i++)
                for (int j = 0; j < minY; j++)
                    for (int k = 0; k < minZ; k++)
                        newArray[i, j, k] = original[i, j, k];
            return newArray;
        }

        public void Resize()
        {
            Data = ResizeArray(Data, Layers, Width, Height);
        }

        public int this[LayerType layer, int x, int y]
        {
            get
            {
                try
                {
                    if (!IsInRegion(x, y))
                    {
                        return Settings.EditorRedCrossTileId ; // Red cross tile
                    }
                    else
                    {
                        return Data[(int)layer, x, y];
                    }
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                if (IsInRegion(x, y))
                {
                    Data[(int)layer, x, y] = value;
                }
            }
        }

        public bool IsWalkable((int x, int y) position) => this[LayerType.Solid, position.x, position.y] == 0;

        public bool IsWalkable(int x, int y) => this[LayerType.Solid, x, y] == 0;

        private bool IsInRegion(int x, int y) => !(x < 0 || x >= Width || y < 0 || y >= Height);

        public void FillLayer(LayerType layer, int tileId)
        {
            Task.Run(() =>
            {
                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        this[layer, x, y] = tileId;
                    }
                }
            });
        }

        public void FillRectangle(LayerType layer, (int x, int y) pos1, (int x, int y) pos2, int tileId)
        {
            Task.Run(() =>
            {
                (int x, int y) = (Math.Min(pos1.x, pos2.x), Math.Min(pos1.y, pos2.y));
                (int w, int h) = (Math.Abs(pos2.x - pos1.x), Math.Abs(pos2.y - pos1.y));

                for (int i = x; i <= x + w; i++)
                {
                    for (int j = y; j <= y + h; j++)
                    {
                        this[layer, i, j] = tileId;
                    }
                }
            });
        }

        public void FloodFill(LayerType layer, int x, int y, int tileId)
        {
            Task.Run(() =>
            {
                var floodTileId = this[layer, x, y];
                var unvisited = new Queue<(int x, int y)>();
                unvisited.Enqueue((x, y));
                var visited = new bool[Width, Height];
                while (unvisited.Count > 0)
                {
                    var current = unvisited.Dequeue();
                    visited[current.x, current.y] = true;
                    this[layer, current.x, current.y] = tileId;

                    foreach (var neighbour in new List<(int x, int y)> { (1, 0), (0, 1), (-1, 0), (0, -1) })
                    {
                        (int x, int y) adjacent = (current.x + neighbour.x, current.y + neighbour.y);
                        if (IsInRegion(adjacent.x, adjacent.y)
                            && !visited[adjacent.x, adjacent.y]
                            && this[layer, adjacent.x, adjacent.y] == floodTileId)
                        {
                            visited[adjacent.x, adjacent.y] = true;
                            unvisited.Enqueue(adjacent);
                        }
                    }
                }
            });
        }

        public void AddItem(Item item, int quantity, (int x, int y) position)
            => Items.Add(new MapItem(item, quantity, position));
        

        public void RemoveItem(Guid id)
        {
            var item = Items.FirstOrDefault(i => i.Id == id);
            Items.Remove(item);
        }

        public void GetItems() => Items.ToList();

        public void AddEntity(Entity entity, (int x, int y) position)
        {
            entity = Helpers.DeepClone(entity);
            entity.Id = Guid.NewGuid();
            entity.CurrentMapId = this.Id;
            entity.Position = position;
            if(entity is Enemy enemy)
            {
                enemy.InitialPosition = entity.Position;
            }
            Entities.Add(entity);
        }

        public void RemoveEntity(Guid id)
        {
            var entity = Entities.FirstOrDefault(e => e.Id == id);
            Entities.Remove(entity);
        }

        public IEnumerable<Entity> GetEntitiesAtPosition((int x, int y) position)
            => Entities.Where(e => e.Position == position).ToList();

        public IEnumerable<(Item Item, int Quantity)> GetItemsAtPosition((int x, int y) position)
            => Items.Where(i => i.Position == position).Select(i => (i.Item, i.Quantity)).ToList();

        public void ClearEntitiesAndItems()
        {
            Entities.Clear();
            Items.Clear();
        }

        public bool IsEmpty((int x, int y) position)
            => GetEntitiesAtPosition(position).Count() == 0;
    }

    public class MapItem
    {
        public Guid Id { get; set; }
        public Item Item { get; set; }
        public int Quantity { get; set; }
        public (int X, int Y) Position { get; set; }

        public MapItem(Item item, int quantity, (int x, int y) position)
        {
            item = Helpers.DeepClone(item);
            item.Id = Guid.NewGuid();
            Id = item.Id;
            Item = item;
            Quantity = quantity;
            Position = position;
        }
    }
}
