using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vault_Scavanger.Classes.Core.InputHandler.ModeHandlers;

public static class InputMessages
{
    public static string DropSuccess() => "Choose an item to drop";
    public static string DropFailure() => "You dont have items to drop";
    public static string MoveUpSuccess(int y, int x) => $"Player moved up from {y} {x}";
    public static string MoveUpFailure() => $"Player can't move up";
    public static string MoveDownSuccess(int y, int x) => $"Player moved Down from {y} {x}";
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
    public static string UnequipBothHands() => "Unequiped item from both hands";
    public static string UnequipFailure() => "You cant unequip an item";
    public static string ExitGame() => "Exiting the game";
    public static string NoFunction() => "This key has no function here";
    public static string UnexpectedBehaviour() => "Something unexpected happened";
}
