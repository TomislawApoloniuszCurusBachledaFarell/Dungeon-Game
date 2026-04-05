using Maze_Mania.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Enums;

namespace Vault_Scavanger.Classes.Utilis;

public static class InputMessages
{
    public static string DropSuccess() => "Choose an item to drop";
    public static string DropFailure() => "You dont have items to drop";
    public static string MoveUpSuccess(int y, int x) => $"Player moved up from {y} {x}";
    public static string MoveUpFailure() => $"Player can't move up";
    public static string MoveDownSuccess(int y, int x) => $"Player moved down from {y} {x}";
    public static string MoveDownFailure() => $"Player can't move down";
    public static string MoveLeftSuccess(int y, int x) => $"Player moved left from {y} {x}";
    public static string MoveLeftFailure() => $"Player can't move left";
    public static string MoveRightSuccess(int y, int x) => $"Player moved right from {y} {x}";
    public static string MoveRightFailure() => $"Player can't move right";
    public static string NoItemToPickUp() => "There is nothing to pick up here";
    public static string FullInventory() => "Inventory is full";
    public static string PickedUpAnItem(string name) => $"Picked up {name}";
    public static string EquipSuccess() => "Select an item to equip";
    public static string EquipFailure() => "You can't equip an item";
    public static string UnequipSuccess() => "Select an Item to unequip";
    public static string UnequipFailure() => "You cant unequip an item";
    public static string EnteredHandSelectionAt(int index) => $"Entered hand selection at index {index}";
    public static string NoItemsInHand(BodyParts bodyPart)
    {
        string partName;
        switch (bodyPart) 
        {
            case BodyParts.LeftHand:
                partName = "left hand";
                break;
            case BodyParts.RightHand:
                partName = "right hand";
                break;
            case BodyParts.BothHands:
                partName = "hands";
                break;
            default:
                partName = "hand";
                break;
        }
        return $"There are no items in {partName}";
    }
    public static string ActionCancelled(InputMode mode)
    {
        string action;
        switch (mode) 
        {
            case InputMode.Drop:
                action = "dropping an item";
                break;
            case InputMode.Equip:
                action = "selecting items";
                break;
            case InputMode.HandSelection:
                action = "selecting hand to equip an item";
                break;
            case InputMode.HandUnequipSelection:
                action = "unequiping items";
                break;
            default:
                action = "an action";
                break;
        }

        return $"Cancelled {action}";
    }

    public static string ItemWasPlacedIn(string name, BodyParts bodyPart)
    {
        string WhereWasPlaced;
        switch (bodyPart)
        {
            case BodyParts.LeftHand:
                WhereWasPlaced = "in left hand";
                break;
            case BodyParts.RightHand:
                WhereWasPlaced = "in right hand";
                break;
            case BodyParts.BothHands:
                WhereWasPlaced = "in both hands";
                break;
            default:
                WhereWasPlaced = "somewhere";
                break;
        }
        return $"{name} was placed {WhereWasPlaced}";
    }

    public static string ItemCouldntBePlaced(string name, BodyParts bodyPart)
    {
        string WhereWasPlaced;
        switch (bodyPart)
        {
            case BodyParts.LeftHand:
                WhereWasPlaced = "in left hand";
                break;
            case BodyParts.RightHand:
                WhereWasPlaced = "in right hand";
                break;
            case BodyParts.BothHands:
                WhereWasPlaced = "in both hands";
                break;
            default:
                WhereWasPlaced = "somewhere";
                break;
        }
        return $"{name} couldn't placed {WhereWasPlaced}";

    }
    public static string UnequipedItem(BodyParts bodyPart)
    {
        string partName;
        switch (bodyPart)
        {
            case BodyParts.LeftHand:
                partName = "left hand";
                break;
            case BodyParts.RightHand:
                partName = "right hand";
                break;
            case BodyParts.BothHands:
                partName = "both hands";
                break;
            default:
                partName = "hand";
                break;
        }
        return $"Item was unequiped from {partName}";
    }
    public static string DrugWasTaken(string name) => $"You have taken {name}";
    public static string ItemHasNoUse() => "You can't use this item";
    public static string ExitGame() => "Exiting the game";
    public static string NoFunction() => "This key has no function here";
    public static string UnexpectedBehaviour() => "Something unexpected happened";
}
