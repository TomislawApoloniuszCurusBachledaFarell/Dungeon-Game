using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Enums;
using Maze_Mania.Interfaces.CoreInterfaces;
using Vault_Scavanger.Classes.Core;
using Vault_Scavanger.Enums;

namespace Maze_Mania.Classes.Core.InputHandlers.ModeHandlers;

public class EquipSelectionHandler : IModeHandler
{
    public InputIResult HandleKey(ConsoleKey key, Player player, Maze maze, KeyDefinitions keyBinds, ref InputMode inputMode, ref int? tempItemIndex)
    {
        InputIResult result = new InputIResult();
        if (ConsoleKey.D0 <= key && key <= ConsoleKey.D9)
        {
            int num = key - ConsoleKey.D0;
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
            if (key == keyBinds.GetActionKey(GameActions.CancelAction))
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
