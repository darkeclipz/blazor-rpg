using DungeonRpg.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonRpg.Services
{
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

        public IEnumerable<Player> GetPlayersAtPosition((int x, int y) position, Guid mapId)
            => Entities.Where(e => e.Position == position && e.CurrentMapId == mapId).ToList();
    }
}
