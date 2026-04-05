using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Classes;
using Maze_Mania.Classes.Core;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Enums;
using Vault_Scavanger.Enums;

namespace Maze_Mania.Interfaces.ItemInterfaces;

public interface IInventoryItem : IItem
{
    public int Value { get; }
    public bool canBeSelected(Inventory inv);
    public InputIResult TrySelecting(Player player, ref InputMode inputMode, int InventoryIndex);
    public InputIResult TryEquipping(Player player, int InventoryIndex, BodyParts bodyPart);
}
