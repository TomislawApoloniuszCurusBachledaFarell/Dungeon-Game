
using Maze_Mania.Classes.Core;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Enums;
using Maze_Mania.Interfaces.CoreInterfaces;
using Maze_Mania.Interfaces.ItemInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Utilis.Messages;
using Vault_Scavanger.Enums;
using Vault_Scavanger.Interfaces.ItemInterfaces;

namespace Vault_Scavanger.Classes.Core.InputHandler.ModeHandlers;

public class AttackHandSelectionHandler : IModeHandler
{
    public InputIResult HandleKey(ConsoleKey key, Player player, Maze maze, KeyDefinitions KeyBinds, ref InputMode inputMode, ref int? tempItemIndex)
    {
        InputIResult result = new InputIResult();
        if (key == KeyBinds.GetActionKey(GameActions.LeftHand) || key == KeyBinds.GetActionKey(GameActions.RightHand))
        {
            tempItemIndex = (int)BodyParts.RightHand;
            if (key == KeyBinds.GetActionKey(GameActions.LeftHand))
            {

                tempItemIndex = (int)BodyParts.LeftHand;
            }
            inputMode = InputMode.Combat;
            IEquipable? Item = player.inventory.ItemInHand((BodyParts)tempItemIndex);
            string? name = null;
            if(Item != null)
            {
                name = Item.Name;
            }
            result.resultMessage = InputMessages.AttackItemSelected(name);
        }
        else if (key == KeyBinds.GetActionKey(GameActions.CancelAction))
        {
            result.resultMessage = InputMessages.ActionCancelled(inputMode);
            inputMode = InputMode.Normal;
        }
        else
        {
            result.resultMessage = InputMessages.NoFunction();

        }
        result.success = true;
        return result;
    }
}
