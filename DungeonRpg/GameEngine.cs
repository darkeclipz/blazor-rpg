using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using DungeonRpg.Engine;
using DungeonRpg.Services;

namespace DungeonRpg
{
    // Game Engine
    public class GameEngine
    {
    }

    // Actions


    // Entity Classes


    // Item Classes


    // Maps


    // Other Classes
    public class Skill : IKey<Skill> 
    {
        public Guid Id { get; set; }
    }



    public enum EffectType { Healing, Attribute, Mana }
    public class Effect : IKey<Effect>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EffectType EffectType { get; set; } 
    }

    public class EffectService : Service<Effect> { }
}
