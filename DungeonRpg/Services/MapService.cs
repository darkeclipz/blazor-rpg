using DungeonRpg.Engine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonRpg.Services
{
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
            StartAiTimers();
        }

        private void StartAiTimers()
        {
            foreach(var map in Entities)
            {
                map.StartAiTimer();
            }
        }

        public override Map New()
        {
            var map = base.New();
            map.Name = "Unnamed map";
            map.Width = 16;
            map.Height = 16;
            map.Resize();
            map.FillLayer(0, Settings.DefaultGrassTileId);
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

        [Obsolete("Method has been moved to the PlayerService.")]
        public IEnumerable<Entity> GetEntitiesAtPosition((int x, int y) position, Guid mapId)
        {
            var entities = new List<Entity>();
            entities.AddRange(PlayerService.All().Where(p => p.Position == position && p.CurrentMapId == mapId));
            entities.AddRange(NpcService.All().Where(n => n.Position == position && n.CurrentMapId == mapId));
            entities.AddRange(EnemyService.All().Where(e => e.Position == position && e.CurrentMapId == mapId));
            return entities;
        }

        public override void InitializeInitialData()
        {
            if (Entities.Count == 0)
            {
                var map = New();
                map.Width = 200;
                map.Height = 200;
                map.Resize();
                map.FillLayer(0, Settings.DefaultGrassTileId);
                map.Name = "Overworld";
            }
        }
    }
}
