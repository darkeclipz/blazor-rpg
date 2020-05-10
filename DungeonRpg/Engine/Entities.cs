using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace DungeonRpg.Engine
{
    public class Attributes
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
        public Attributes Attributes { get; set; } = new Attributes();
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public (int X, int Y) Position = (0, 0);
        public Guid CurrentMapId { get; set; }
    }

    public abstract class Character : Entity 
    { 
        public Gender Gender { get; set; }
        public Race Race { get; set; }
    }
    public class Enemy : Entity, IKey<Enemy> { }
    public class Player : Character, IKey<Player>
    {
        public string Password { get; set; }
        public bool IsAdministrator { get; set; }
    }
    public class Npc : Character, IKey<Npc> { }
}
