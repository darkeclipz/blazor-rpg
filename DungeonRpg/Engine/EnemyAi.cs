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
#pragma warning disable CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).
                (int x, int y) = Probability.Random.Next(0, 4) switch
#pragma warning restore CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).
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
                var walkable = map[Map.LayerType.Solid, targetPosition.x, targetPosition.y] == 0;

                if (walkable && distance <= enemy.WalkRadius)
                {
                    enemy.Position = targetPosition;
                    break;
                }
            }
        }
    }
}
