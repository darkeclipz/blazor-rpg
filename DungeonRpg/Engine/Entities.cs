﻿using DungeonRpg.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace DungeonRpg.Engine
{
    [Serializable]
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

    [Serializable]
    public class Race : IKey<Race>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int MaleTileId { get; set; }
        public int FemaleTileId { get; set; }
    }

    [Serializable]
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
    [Serializable]
    public abstract class Character : Entity 
    { 
        public Gender Gender { get; set; }
        public Race Race { get; set; } = new Race();
    }
    [Serializable]
    public class Enemy : Entity, IKey<Enemy> 
    {
        public int TileId { get; set; }
    }
    [Serializable]
    public class Player : Character, IKey<Player>
    {
        public string Password { get; set; }
        public bool IsAdministrator { get; set; }
    }
    [Serializable]
    public class Npc : Character, IKey<Npc> { }
}
