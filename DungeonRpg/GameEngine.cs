using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace DungeonRpg
{
    // Game Engine
    public class GameEngine
    {
    }

    // Actions
    public abstract class Action
    { 
    }

    public class MoveAction : Action { }
    public class EquipAction : Action { }
    public class UnequipAction : Action { }
    public class AttackAction : Action { }
    public class TeleportAction : Action { }

    // Entity Classes
    public struct Attributes
    {
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Memory { get; set; }
    }

    public enum Gender { Male, Female }
    public enum Race { Human, Elf, Orc, Draconian }

    public abstract class Entity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Experience { get; set; }
        public Attributes Attributes { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
    }

    public abstract class Character : Entity { }
    public class Enemy : Character, IKey<Enemy> { }
    public class Player : Character, IKey<Player>
    {
        public string Password { get; internal set; }
    }
    public class Npc : Character, IKey<Npc> { }

    // Item Classes
    public enum ItemType { Weapon, Armor, Jewelry, Consumable, Material, Book, Item }
    public class Item : IKey<Item>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public static ItemType GetItemType(Item item)
        {
            if (item is Weapon) return ItemType.Weapon;
            else if (item is Armor) return ItemType.Armor;
            else if (item is Jewelry) return ItemType.Jewelry;
            else if (item is Material) return ItemType.Material;
            else if (item is Book) return ItemType.Book;
            else if (item is Consumable) return ItemType.Consumable;
            else return ItemType.Item;
        }
    }

    public abstract class Equipment : Item { }
    public class Armor : Equipment { }
    public class Weapon : Equipment { }
    public class Jewelry : Equipment { }
    public class Consumable : Item { }
    public class Book : Item 
    { 
        public int Pages { get; set; }
    }
    public class Material : Item { }

    // Maps
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

        public Map()
        {
            Data = new int[Layers, Width, Height];
        }

        private T[,,] ResizeArray<T>(T[,,] original, int x, int y, int z)
        {
            var newArray = new T[x, y, z];
            int minX = Math.Min(x, original.GetLength(0));
            int minY = Math.Min(y, original.GetLength(1));
            int minZ = Math.Min(z, original.GetLength(2));
            for (int i = 0; i < minX; i++)
                for (int j = 0; j < minY; j++)
                    for(int k=0; k < minZ; k++)
                        newArray[i, j, k] = original[i, j, k];
            return newArray;
        }

        public void Resize()
        {
            Data = ResizeArray(Data, Layers, Width, Height);
        }

        public int this[int layer, int x, int y]
        {
            get
            {
                try
                {
                    if (!IsInRegion(x, y))
                    {
                        return 4839; // Red cross tile
                    }
                    else
                    {
                        return Data[layer, x, y];
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
                    Data[layer, x, y] = value;
                }
            }
        }

        private bool IsInRegion(int x, int y) => !(x < 0 || x >= Width || y < 0 || y >= Height);
        
        public void FillLayer(int layer, int tileId)
        {
            for(int x = 0; x < Width; x++)
            {
                for(int y = 0; y < Height; y++)
                {
                    this[layer, x, y] = tileId;
                }
            }
        }

        public void FloorFill(int layer, int x, int y, int tileId)
        {
            var floodTileId = Data[layer, x, y];
            var unvisited = new Queue<(int x, int y)>();
            unvisited.Enqueue((x, y));
            var visited = new bool[Width, Height];
            while(unvisited.Count > 0)
            {
                var current = unvisited.Dequeue();
                visited[current.x, current.y] = true;
                Data[layer, current.x, current.y] = tileId;

                foreach (var neighbour in new List<(int x, int y)> { (1, 0), (0, 1), (-1, 0), (0, -1) })
                {
                    (int x, int y) adjacent = (current.x + neighbour.x, current.y + neighbour.y);
                    if (IsInRegion(adjacent.x, adjacent.y)
                        && !visited[adjacent.x, adjacent.y]
                        && Data[layer, adjacent.x, adjacent.y] == floodTileId)
                    {
                        unvisited.Enqueue(adjacent);
                    }
                }
            }
        }
    }

    // Other Classes
    public class Skill : IKey<Skill> 
    {
        public Guid Id { get; set; }
    }

    public interface IKey<T> 
    { 
        Guid Id { get; set; } 
    }

    // Services
    public abstract class Service<T> where T : IKey<T>, new()
    {
        protected IList<T> Entities { get; set; }

        public Service()
        {
            Entities = new List<T>();
            Load();
        }

        public virtual IEnumerable<T> All()
        {
            return Entities.ToList();
        }

        public virtual T Find(Guid id)
        {
            return Entities.FirstOrDefault(entity => entity.Id == id);
        }

        public virtual T New()
        {
            var entity = new T();
            return Add(entity);
        }

        public virtual T Add(T entity)
        {
            entity.Id = Guid.NewGuid();
            Entities.Add(entity);
            return entity;
        }

        public virtual T Update(T entity)
        {
            var storedEntity = Find(entity.Id);
            Entities.Remove(storedEntity);
            Entities.Add(entity);
            return entity;
        }

        public virtual void Remove(T entity)
        {
            Entities.Remove(entity);
        }

        protected string GetFileName()
        {
            var name = Entities.GetType().FullName;
            var start = name.IndexOf("[[") + 13;
            var stop = name.IndexOf(",") - start;
            var type = name.Substring(start, stop).ToLower();
            return type + ".service.data";
        }

        protected JsonSerializerSettings GetSerializerSettings()
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            return settings;
        }

        public virtual void Save()
        {
            var settings = GetSerializerSettings();
            var json = JsonConvert.SerializeObject(Entities, settings);
            var fileName = GetFileName();
            File.WriteAllText(fileName, json);
        }

        protected virtual void Load()
        {
            var fileName = GetFileName();
            if(File.Exists(fileName))
            {
                var json = File.ReadAllText(fileName);
                var settings = GetSerializerSettings();
                Entities = JsonConvert.DeserializeObject<List<T>>(json, settings);
            }
            else
            {
                Save();
            }
        }
    }

    public class ItemService : Service<Item> 
    {
        public Item New(ItemType type = ItemType.Item) =>
            type switch
            {
                ItemType.Weapon => Add(new Weapon()),
                ItemType.Armor => Add(new Armor()),
                ItemType.Jewelry => Add(new Jewelry()),
                ItemType.Consumable => Add(new Consumable()),
                ItemType.Material => Add(new Material()),
                ItemType.Book => Add(new Book()),
                _ => Add(new Item()),
            };
    }

    public class PlayerService : Service<Player>
    {
        public Player FindByName(string username)
        {
            return Entities.FirstOrDefault(entity => entity.Name.ToLower() == username.ToLower());
        }
    }
    public class NpcService : Service<Npc> { }
    public class SkillService : Service<Skill> { }
    public class EnemyService : Service<Enemy> { }
    public class MapService : Service<Map> 
    {
        public override Map New()
        {
            var map = base.New();
            map.Name = "Unnamed map";
            map.Width = 16;
            map.Height = 16;
            map.Resize();
            int grassTileId = 959;
            map.FillLayer(0, grassTileId);
            return map;
        }

        public override void Save()
        {
            var settings = GetSerializerSettings();
            // There is an error while serializing to a file. The array
            // is stored as single-dimension int[,], but is supposed
            // to be a multi-dimensional int[,,]. This is fixed here.
            var json = JsonConvert.SerializeObject(Entities, settings);
            var patch = "k__BackingField\":{\"$type\":\"System.Int32[,]";
            var target = "k__BackingField\":{\"$type\":\"System.Int32[,,]";
            json = json.Replace(patch, target);
            var fileName = GetFileName();
            File.WriteAllText(fileName, json);
        }
    }
}
