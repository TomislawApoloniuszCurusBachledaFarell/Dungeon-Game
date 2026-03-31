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
using Vault_Scavanger.Interfaces.ItemInterfaces;

namespace Maze_Mania.Classes.Core.InputHandlers.ModeHandlers;

public class HandUnequipSelectionHandler : IModeHandler
{
    public InputIResult HandleKey(ConsoleKey key, Player player, Maze maze, KeyDefinitions KeyBinds, ref InputMode inputMode, ref int? tempItemIndex)
    {
        InputIResult result = new InputIResult();
        if (key == KeyBinds.GetActionKey(GameActions.LeftHand) || key == KeyBinds.GetActionKey(GameActions.RightHand))
        {
            BodyParts bodyPart = key == KeyBinds.GetActionKey(GameActions.LeftHand) ? BodyParts.LeftHand : BodyParts.RightHand;
            IEquipable? item = player.inventory.ItemInHand(bodyPart);
            if (item != null) 
            {
                result.success = true;
                result.resultMessage = InputMessages.NoItemsInHand(bodyPart);
                return result;
            }
            char c = key == KeyBinds.GetActionKey(GameActions.LeftHand) ? 'l' : 'r';
            result = player.Unequip(c);
            if (result.success)
                inputMode = InputMode.Normal;
        }
        else if (key == KeyBinds.GetActionKey(GameActions.CancelAction))
        {
            result.resultMessage = "Cancelled selecting hand to unequip items from";
            inputMode = InputMode.Normal;
        }
        else
        {
            result.resultMessage = "This key has no function here";
        }
        result.success = true;
        return result;
    }

}
