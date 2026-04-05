using Maze_Mania.Classes.Core;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Core.InputHandler.ModeHandlers;
using Vault_Scavanger.Classes.Utilis;
using Vault_Scavanger.Enums;

namespace Vault_Scavanger.Classes.Core.InputHandler.ModeActions.NormalModeCOR;

public class UnequipAction : NormalHandler
{
    public UnequipAction() : base() { }
    protected override bool canHandle(ConsoleKey PressedKey, KeyDefinitions keyBinds)
    {
        return PressedKey == keyBinds.GetActionKey(GameActions.Unequip);
    }

    protected override InputIResult Process(ConsoleKey ckey, Player player, Maze maze, KeyDefinitions KeyBinds, ref InputMode inputMode, ref int? tempItemIndex)
    {
        InputIResult result = new InputIResult() {success = true };
        if ((player.isLeftHandOccupied() || player.isRightHandOccupied()) && player.hasInventorySpace())
        {
            if (player.isLeftHandOccupied() && player.isRightHandOccupied() && !player.IsTwoHandedEquipped())
            {
                inputMode = InputMode.HandUnequipSelection;
                result.resultMessage = InputMessages.UnequipSuccess();
            }
            else
            {
                BodyParts bodyPart = 0;
                if (player.IsTwoHandedEquipped())
                    bodyPart = BodyParts.BothHands;
                else if (player.isLeftHandOccupied())
                    bodyPart = BodyParts.LeftHand;
                else if (player.isRightHandOccupied())
                    bodyPart = BodyParts.RightHand;
                result = player.inventory.ItemInHand(bodyPart).Unequip(player, bodyPart);
            }

        }
        else
        {
            result.resultMessage = InputMessages.UnequipFailure();
        }
        return result;
    }
}
