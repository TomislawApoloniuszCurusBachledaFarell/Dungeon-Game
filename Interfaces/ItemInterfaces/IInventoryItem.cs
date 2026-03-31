using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Classes;
using Maze_Mania.Classes.Core;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Enums;

namespace Maze_Mania.Interfaces.ItemInterfaces;

public interface IInventoryItem : IItem
{
    public int Value { get; }
    public InputIResult TrySelecting(Player player, InputMode inputMode, int InventoryIndex);
}
