using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Enums;
using Maze_Mania.Interfaces.CoreInterfaces;
using Maze_Mania.Interfaces.ItemInterfaces;
using Vault_Scavanger.Classes.Core;
using Vault_Scavanger.Classes.Core.InputHandler.ModeHandlers;
using Vault_Scavanger.Enums;

namespace Maze_Mania.Classes.Core.InputHandlers.ModeHandlers;

public class HandSelectionHandler : IModeHandler
{
    public InputIResult HandleKey(ConsoleKey key, Player player, Maze maze, KeyDefinitions KeyBinds, ref InputMode inputMode, ref int? tempItemIndex)
    {
        if(tempItemIndex == null) 
        { 
            return new InputIResult { resultMessage = InputMessages.UnexpectedBehaviour(), success = true }; 
        }
        InputIResult result = new InputIResult();
        if (key == KeyBinds.GetActionKey(GameActions.LeftHand) || key == KeyBinds.GetActionKey(GameActions.RightHand))
        {
            IInventoryItem item = player.inventory.items[(int) tempItemIndex];
            BodyParts bodyPart = BodyParts.RightHand;
            if (key == KeyBinds.GetActionKey(GameActions.LeftHand))
            {
                bodyPart = BodyParts.LeftHand;
            }
            result = item.TryEquipping(player.inventory, (int) tempItemIndex, bodyPart);
            if (result.success)
            {
                inputMode = InputMode.Normal;
                tempItemIndex = null;
            }
        }
        else if (key == KeyBinds.GetActionKey(GameActions.CancelAction))
        {
            result.resultMessage = "Cancelled selecting hands";
            inputMode = InputMode.Equip;
        }
        else
        {
            result.resultMessage = "This key has no function here";

        }
        result.success = true;
        return result;
    }

}
