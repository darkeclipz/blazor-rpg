using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonRpg.Engine
{
    public static class Settings
    {
        public static string DefaultMapName { get; set; } = "Overworld";
        public static (int X, int Y) DefaultPosition { get; set; } = (8, 8);
        public static int DefaultGrassTileId { get; set; } = 6739;
        public static int EditorRedCrossTileId { get; set; } = 4839;
    }

    public static class Rules
    {
        // Mathematical model rules.
    }
}
