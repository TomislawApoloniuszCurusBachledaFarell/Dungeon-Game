using Maze_Mania.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Core;
using Vault_Scavanger.Enums;

namespace Vault_Scavanger.Classes.Utilis;

public class InteractionMessages
{
    public KeyDefinitions keys;

    public InteractionMessages(KeyDefinitions keys)
    {
        this.keys = keys;
    }

    public string ExitMessage()
    {
        return $"Press {keys.GetActionKey(GameActions.GameExit)} to Exit the game";
    }

    public string CancelMessage()
    {
        return $"Press {keys.GetActionKey(GameActions.CancelAction)} to cancel";
    }

    public string PickUpMessage(string name)
    {
        return $"Press {keys.GetActionKey(GameActions.PickUp)} to pick up {name}";
    }

    public string DropMessage() 
    {
        return $"Press {keys.GetActionKey(GameActions.Drop)} to drop an item";
    }
    public string SelectAttackType(char key, string name) => $"Press {key} to make perform {name}";
    public string SelectItemMessage(char key, string name, InputMode mode) 
    {
        string interaction = "";
        switch (mode)
        {
            case InputMode.Drop:
                interaction = "drop";
                break;
            case InputMode.Equip:
                interaction = "use";
                break;
            default:
                interaction = "select";
                break;

        }
        return $"Press {key} to {interaction} {name}";
    }

    public string EquipMessage()
    {
        return $"Press {keys.GetActionKey(GameActions.Equip)} to equip an item";
    }

    public string LeftHandMessage(InputMode inputMode)
    {
        string interaction = "";
        string additionalInfo = "";
        switch (inputMode)
        {
            case InputMode.HandSelection:
                interaction = "equip it to your";
                break;
            case InputMode.HandUnequipSelection:
                interaction = "unequip it from your";
                break;
            case InputMode.AttackHandSelection:
                interaction = "attack with your";
                additionalInfo = "item";
                break;
            default:
                interaction = "select";
                break;
        }
        return $"Press {keys.GetActionKey(GameActions.LeftHand)} to {interaction} left hand {additionalInfo}";
    }

    public string RightHandMessage(InputMode inputMode) 
    {
        string interaction = "";
        string additionalInfo = "";
        switch (inputMode)
        {
            case InputMode.HandSelection:
                interaction = "equip it to your";
                break;
            case InputMode.HandUnequipSelection:
                interaction = "unequip it from your";
                break;
            case InputMode.AttackHandSelection:
                interaction = "attack with your";
                additionalInfo = "item";
                break;
            default:
                interaction = "select";
                break;
        }
        return $"Press {keys.GetActionKey(GameActions.RightHand)} to {interaction} right hand {additionalInfo}";
    }

    public string UnequipMessage()
    {
        return $"Press {keys.GetActionKey(GameActions.Unequip)} to unequip an item";
    }
}
