using Maze_Mania.Classes.Core;
using Maze_Mania.Classes.Items.Currency;
using Maze_Mania.Enums;
using Maze_Mania.Interfaces.ItemInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Utilis;
using Vault_Scavanger.Interfaces.CoreInterfaces;

namespace Vault_Scavanger.Classes.Core.InteractionFinder.InteractionRules;

public class PickUpRule : IInteractionRule
{
    public PickUpRule()
    {
    }

    public bool canInteract(Maze maze, Player player, InputMode inputMode)
    {
        IItem? item = maze.ItemBoard[player.yPos, player.xPos].FirstOrDefault();

        return (inputMode == InputMode.Normal && item != null && (player.hasInventorySpace() || item.Name == GoldBar.getName || item.Name == BottleCap.getName));
    }


    public List<string> GetInteractions(Maze maze, Player player, InputMode inputMode, InteractionMessages interactionMessages)
    {
        List<string> result = new List<string>();
        if (canInteract(maze, player, inputMode))
        {
            IItem? item = maze.ItemBoard[player.yPos, player.xPos].FirstOrDefault();
            string name = item != null ? item.Name : "an item";
            result.Add(interactionMessages.PickUpMessage(name));
        }
        return result;
    }
}
