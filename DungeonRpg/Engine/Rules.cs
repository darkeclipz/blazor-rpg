using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonRpg.Engine
{
    public static class Rules
    {
        public static string DefaultMapName { get; set; } = "Overworld";
        public static (int X, int Y) DefaultPosition { get; set; } = (8, 8);
    }
}
