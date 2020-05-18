using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonRpg.Engine
{
    public static class EnemyAi
    {
        public static void ExecuteRandomWalkInRadius(Enemy enemy, Map map)
        {
            int retries = 10;
            while (retries-- > 0)
            {
                // Get a random direction
                (int x, int y) = Probability.Uniform(0, 3) switch
                {
                    0 => (0, 1),
                    1 => (0, -1),
                    2 => (1, 0),
                    3 => (-1, 0)
                };

                // Check if that tile is reachable
                (int x, int y) targetPosition = (enemy.Position.X + x, enemy.Position.Y + y);
                var distance = Math.Abs(targetPosition.x - enemy.InitialPosition.X) 
                    + Math.Abs(targetPosition.y - enemy.InitialPosition.Y);
                var isInRadius = distance <= enemy.WalkRadius;

                if (isInRadius && map.IsWalkable(targetPosition))
                {
                    enemy.Position = targetPosition;
                    break;
                }
            }
        }
    }
}
