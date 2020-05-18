using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonRpg.Services
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

        public virtual void Remove(T entity)
        {
            Entities.Remove(entity);
        }

        public virtual int Count()
        {
            return Entities.Count;
        }

        protected string GetFileName()
        {
            var name = Entities.GetType().FullName;
            var start = name.IndexOf("[[") + 20;
            var stop = name.IndexOf(",") - start;
            var type = name.Substring(start, stop).ToLower();
            return "Data/" + type + ".data.json";
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

        public virtual void InitializeInitialData() { }
    }
}
