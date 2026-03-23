using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Enums;
using Maze_Mania.Interfaces.CoreInterfaces;
using Maze_Mania.Classes.Core.InputHandlers.ModeHandlers;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Interfaces.ItemInterfaces;

namespace Maze_Mania.Classes.Core.InputHandlers.ModeHandlers;

public class NormalHandler : IModeHandler
{
    public InputIResult HandleKey(char key, Player player, Maze maze, ref InputMode inputMode, ref int? tempItemIndex)
    {
        InputIResult result = new InputIResult();
        switch (key)
        {
            case 'a':
                result = maze.MoveLeft();
                break;
            case 'd':
                result = maze.MoveRight();
                break;
            case 'w':
                result = maze.MoveUp();
                break;
            case 's':
                result = maze.MoveDown();
                break;
            case 'e':
                    result.resultMessage = maze.PickUp().resultMessage;
                break;
            case 'g':
                if ((player.isLeftHandOccupied() || player.isRightHandOccupied()) && player.hasInventorySpace())
                {
                    if (player.isLeftHandOccupied() && player.isRightHandOccupied() && !player.IsTwoHandedEquipped())
                    {
                        inputMode = InputMode.HandUnequipSelection;
                        result.resultMessage = $"Choose which item to unequip";
                    }
                    else
                    {
                        player.Unequip('l');
                        player.Unequip('r');
                        result.resultMessage = $"Unequiped item from both hands";
                    }

                }
                else
                {
                    result.resultMessage = "This key has no function here";
                }

                break;
            case 'f':
                if (player.HasEquipable())
                {
                    inputMode = InputMode.Equip;
                    result.resultMessage = $"Choose an item to equip";
                }
                else
                {
                    result.resultMessage = "This key has no function here";
                }
                break;
            case 'q':
                if (player.isDropPossible() != DropOptions.None)
                {
                    inputMode = InputMode.Drop;
                    result.resultMessage = "Choose an item to drop";
                }
                else
                {
                    result.resultMessage = "This key has no function here";
                }
                break;
            case 'x':
                result.resultMessage = "Exited the game";
                result.success = false;
                return result;
            default:
                result.resultMessage = "This key has no function here";
                break;
        }
        result.success = true;
        return result;
    }
}
