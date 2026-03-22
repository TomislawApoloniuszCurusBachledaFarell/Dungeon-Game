using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Enums;
using Maze_Mania.Interfaces.CoreInterfaces;
using Maze_Mania.Classes.Core.InputHandlers.ModeHandlers;

namespace Maze_Mania.Classes.Core.InputHandlers.ModeHandlers;

public class NormalHandler : IModeHandler
{
    public bool HandleKey(char key, Player player, Maze maze, ref InputMode inputMode, ref int? tempItemIndex)
    {
        switch (key)
        {
            case 'a':
                maze.MoveLeft();
                break;
            case 'd':
                maze.MoveRight();
                break;
            case 'w':
                maze.MoveUp();
                break;
            case 's':
                maze.MoveDown();
                break;
            case 'e':
                string? name = maze.ItemBoard[player.yPos, player.xPos].FirstOrDefault()?.Name;
                if (player.hasInventorySpace() || name == "Gold Bar" || name == "Bottle Cap")
                    maze.PickUp();
                break;
            case 'g':
                if ((player.isLeftHandOccupied() || player.isRightHandOccupied()) && player.hasInventorySpace())
                    if (player.isLeftHandOccupied() && player.isRightHandOccupied() && !player.IsTwoHandedEquipped())
                        inputMode = InputMode.HandUnequipSelection;
                    else
                    {
                        player.Unequip('l');
                        player.Unequip('r');
                    }
                break;
            case 'f':
                if (player.HasEquipable())
                    inputMode = InputMode.Equip;
                break;
            case 'q':
                if (player.isDropPossible() != DropOptions.None)
                    inputMode = InputMode.Drop;
                break;
            case 'x':
                return false;
            default:
                break;
        }
        return true;
    }
}
