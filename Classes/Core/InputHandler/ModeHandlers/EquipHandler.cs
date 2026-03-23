using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Enums;
using Maze_Mania.Interfaces.CoreInterfaces;

namespace Maze_Mania.Classes.Core.InputHandlers.ModeHandlers;

public class EquipHandler : IModeHandler
{
    public InputIResult HandleKey(char key, Player player, Maze maze, ref InputMode inputMode, ref int? tempItemIndex)
    {
        InputIResult result = new InputIResult();
        if (char.IsDigit(key))
        {
            int num = key - '0';
            if (player.isTwoHanded(num))
            {
                if (player.CanEquipTwoHanded(num)) {
                    result = player.Equip(num, '?');
                    if(result.success)
                        inputMode = InputMode.Normal;
                }
            }
            else
            {
                if (num < player.getAllItemNames().Count)
                {
                    tempItemIndex = num;
                    inputMode = InputMode.HandSelection;
                    result.resultMessage = $"Entered hand selections to equip item at {key} index";

                }
                else
                {
                    result.resultMessage = $"Player does not have any items at index {key} to equip";
                }
            }
        }
        else
        {
            if (key == 'n')
            {
                inputMode = InputMode.Normal;
                result.resultMessage = "Cancelled equipping items";
            }
            else 
            {
                result.resultMessage = "This key has no function here";

            }
        }
        result.success = true;
        return result;
    }


}
