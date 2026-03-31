using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Enums;
using Maze_Mania.Interfaces.CoreInterfaces;
using Vault_Scavanger.Classes.Core;
using Vault_Scavanger.Classes.Core.InputHandler.ModeHandlers;
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
            if (num < player.inventory.items.Count) 
            {
                result = player.inventory.items[num].TrySelecting(player, inputMode, num);
                if(result.success == true)
                {
                    tempItemIndex = num;
                    result.resultMessage = InputMessages.EnteredHandSelectionAt(num);
                    return result;
                }
                else
                {
                    result.success = true;
                    return result;
                }
            }
           
        }
        else
        {
            if (key == keyBinds.GetActionKey(GameActions.CancelAction))
            {
                inputMode = InputMode.Normal;
                result.resultMessage = InputMessages.ActionCancelled(inputMode);
            }
            else 
            {
                result.resultMessage = InputMessages.NoFunction();

            }
        }
        result.success = true;
        return result;
    }


}
