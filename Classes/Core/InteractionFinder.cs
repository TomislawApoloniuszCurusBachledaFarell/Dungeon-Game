using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Classes.Items.Currency;
using Maze_Mania.Enums;

namespace Maze_Mania.Classes.Core;

public static class InteractionFinder
{
    public static List<string> FindInteractions(Maze maze, Player player, InputMode inputMode)
    {
        List<string> Interactions = new List<string>();
        DropOptions dropOption = player.isDropPossible();

        switch (inputMode)
        {
            case InputMode.Normal:
                string? name = maze.ItemBoard[player.yPos, player.xPos].FirstOrDefault()?.Name;
                if (maze.board[player.yPos, player.xPos] != ' ' && (player.hasInventorySpace() || name == "Gold Bar" || name == "Bottle Cap"))
                    Interactions.Add($"press E to pick up {maze.ItemBoard[player.yPos, player.xPos].First().Name}");
                if (dropOption != DropOptions.None)
                {
                    if (player.HasEquipable())
                        Interactions.Add($"press F to equip an item from your inventory");
                    Interactions.Add($"press Q to drop an item from your inventory");
                }
                if ((player.isLeftHandOccupied() || player.isRightHandOccupied()) && player.hasInventorySpace())
                    Interactions.Add($"press G to unequip an item from your hand");

                Interactions.Add($"press X to exit the game");
                break;
            case InputMode.Drop:

                if ((dropOption & DropOptions.Item) != 0)
                {
                    List<string> availableItemss = player.getAllItemNames();
                    int i = 0;
                    foreach (string availableItem in availableItemss)
                    {
                        Interactions.Add($"press {i} to drop {availableItem}");
                        i++;
                    }

                }
                if ((dropOption & DropOptions.BottleCap) != 0)
                    Interactions.Add($"press C to drop a bottle cap");
                if ((dropOption & DropOptions.GoldBar) != 0)
                    Interactions.Add($"press G to drop a gold bar");
                Interactions.Add("press N to cancel");
                break;
            case InputMode.Equip:
                List<string> availableItems = player.getAllItemNames();
                List<bool> isItemTwoHanded = player.getAllItemHandability();
                for (int i = 0; i < availableItems.Count; i++)
                {
                    if (player.inventory.MaxCapacity == availableItems.Count && isItemTwoHanded[i] && player.isLeftHandOccupied() && player.isRightHandOccupied())
                        continue;
                    Interactions.Add($"press {i} to equip {availableItems[i]}");
                }

                Interactions.Add("press N to cancel");
                break;
            case InputMode.HandSelection:
                Interactions.Add("press R to equip it to your right hand");
                Interactions.Add("press L to equip it to your left hand");

                Interactions.Add("press N to cancel");
                break;
            case InputMode.HandUnequipSelection:
                if (player.isLeftHandOccupied())
                    Interactions.Add("press L to unequip item from your left hand");

                if (player.isRightHandOccupied())
                    Interactions.Add("press R to unequip item from your right hand");

                Interactions.Add("press N to cancel");
                break;
        };
        return Interactions;
    }
}
