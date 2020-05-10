using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonRpg.Engine
{
    public abstract class Action
    {
    }

    public class MoveAction : Action { }
    public class EquipAction : Action { }
    public class UnequipAction : Action { }
    public class AttackAction : Action { }
    public class TeleportAction : Action { }
}