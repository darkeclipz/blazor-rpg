using DungeonRpg.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonRpg.Services
{
    public class ItemService : Service<Item>
    {
        public Item New(ItemType type = ItemType.Item) =>
            type switch
            {
                ItemType.Weapon => Add(new Weapon()),
                ItemType.Armor => Add(new Armor()),
                ItemType.Jewelry => Add(new Jewelry()),
                ItemType.Consumable => Add(new Consumable()),
                ItemType.Material => Add(new Material()),
                ItemType.Book => Add(new Book()),
                _ => Add(new Item()),
            };

        public override void InitializeInitialData()
        {
            if (Entities.Count == 0)
            {
                var weapon = New(ItemType.Weapon) as Weapon;
                weapon.Name = "Sword";
                weapon.Description = "Just a sword!";
                weapon.TileId = 1812;
                weapon.EquippedTileId = 2340;
                weapon.Value = 100;
            }
        }
    }
}
