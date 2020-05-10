using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonRpg.Engine
{
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
            if (File.Exists(fileName))
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
        public override Player New()
        {
            var player = new Player();
            if (Entities.Count == 0)
            {
                player.IsAdministrator = true;
            }
            return Add(player);
        }

        public Player FindByName(string username)
        {
            return Entities.FirstOrDefault(entity => entity.Name.ToLower() == username.ToLower());
        }
    }
    public class NpcService : Service<Npc>
    {
    }
    public class EnemyService : Service<Enemy>
    {
    }
    public class MapService : Service<Map>
    {
        public PlayerService PlayerService { get; }
        public NpcService NpcService { get; }
        public EnemyService EnemyService { get; }

        public MapService(PlayerService playerService, NpcService npcService, EnemyService enemyService)
        {
            PlayerService = playerService;
            NpcService = npcService;
            EnemyService = enemyService;
        }

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

        public Map FindByName(string name) => Entities.FirstOrDefault(e => e.Name == name);

        public IEnumerable<Entity> EntitiesAtPosition(int x, int y, Guid mapId)
            => GetEntitiesAtPosition((x, y), mapId);

        public IEnumerable<Entity> GetEntitiesAtPosition((int x, int y) position, Guid mapId)
        {
            var entities = new List<Entity>();
            entities.AddRange(PlayerService.All().Where(p => p.Position == position && p.CurrentMapId == mapId));
            entities.AddRange(NpcService.All().Where(n => n.Position == position && n.CurrentMapId == mapId));
            entities.AddRange(EnemyService.All().Where(e => e.Position == position && e.CurrentMapId == mapId));
            return entities;
        }
    }
}
