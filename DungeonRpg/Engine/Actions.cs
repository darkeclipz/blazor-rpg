using DungeonRpg.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DungeonRpg.Engine.Map;
using static DungeonRpg.Engine.MoveAction;

namespace DungeonRpg.Engine
{
    [Serializable]
    public abstract class Action
    {
        public virtual void Execute(ActionFactory factory) { }
    }

    [Serializable]
    public class MoveAction : Action
    {
        private readonly Player player;
        private readonly MoveActionDirection direction;
        public MoveAction(Player player, MoveActionDirection direction)
        {
            this.player = player;
            this.direction = direction;
        }
        public enum MoveActionDirection { Left, Up, Down, Right };
        public override void Execute(ActionFactory factory)
        {
            (int x, int y) position = direction switch
            {
                MoveActionDirection.Up => (player.Position.X, player.Position.Y - 1),
                MoveActionDirection.Left => (player.Position.X - 1, player.Position.Y),
                MoveActionDirection.Down => (player.Position.X, player.Position.Y + 1),
                MoveActionDirection.Right => (player.Position.X + 1, player.Position.Y),
                _ => player.Position
            };
            var map = factory.MapService.Find(player.CurrentMapId);
            if(map.IsWalkable(position))
            {
                player.Position = position;
            }
        }
    }
    public class EquipAction : Action { }
    public class UnequipAction : Action { }
    public class AttackAction : Action { }
    public class TeleportAction : Action { }
    public class UpgradeAction : Action { }
    public class ChatAction : Action { }
    public class CraftAction : Action { }
    public class ShopAction : Action { }
    public class BankAction : Action { }
    public class DialogAction : Action { }

    public class ActionFactory
    {
        public PlayerService PlayerService { get; }
        public ItemService ItemService { get; }
        public MapService MapService { get; }

        public ActionFactory(PlayerService playerService, ItemService itemService, MapService mapService)
        {
            PlayerService = playerService;
            ItemService = itemService;
            MapService = mapService;
        }

        public void ExecuteMoveAction(Player player, MoveActionDirection direction)
            => new MoveAction(player, direction).Execute(this);
    }
}