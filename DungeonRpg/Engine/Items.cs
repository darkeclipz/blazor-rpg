using DungeonRpg.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonRpg.Engine
{
    public enum ItemType { Weapon, Armor, Jewelry, Consumable, Material, Book, Item }
    public enum ItemGrade { Common, Rare, Unique, Legendary }
    [Serializable]
    public class Item : IKey<Item>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }
        public int Level { get; set; }
        public int TileId { get; set; }
        public ItemGrade Grade { get; set; }
        public bool Discardable { get; set; } = true;
        public bool Sellable { get; set; } = true;

        public static ItemType GetItemType(Item item) => item switch
        {
            Weapon _ => ItemType.Weapon,
            Armor _ => ItemType.Armor,
            Jewelry _ => ItemType.Jewelry,
            Material _ => ItemType.Material,
            Book _ => ItemType.Book,
            Consumable _ => ItemType.Consumable,
            _ => ItemType.Item
        };
    }
    [Serializable]
    public abstract class Equipment : Item
    {
        public Attributes AttributesRequired { get; set; } = new Attributes();
        public int EnchantmentLevel { get; set; }
        public int EquippedTileId { get; set; }
    }

    public enum ArmorType { Shield, Headwear, Body, Gloves, Boots }
    [Serializable]
    public class Armor : Equipment
    {
        public ArmorType ArmorType { get; set; }
        public int Defense { get; set; }
    }

    public enum WeaponType { OneHanded, TwoHanded };
    public enum DamageType { Physical, Magic };
    [Serializable]
    public class Weapon : Equipment
    {
        public WeaponType WeaponType { get; set; }
        public DamageType DamageType { get; set; }
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
    }

    public enum JewelryType { Necklace, Ring, Earring }
    [Serializable]
    public class Jewelry : Equipment
    {
        public JewelryType JewelryType { get; set; }
    }
    [Serializable]
    public class Consumable : Item { }
    [Serializable]
    public class Book : Item
    {
        public int Pages { get; set; }
    }
    [Serializable]
    public class Material : Item { }
}
