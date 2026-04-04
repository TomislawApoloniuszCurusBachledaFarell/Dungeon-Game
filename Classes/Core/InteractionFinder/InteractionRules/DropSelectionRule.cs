using Maze_Mania.Classes.Core;
using Maze_Mania.Classes.Items.Currency;
using Maze_Mania.Enums;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Utilis;
using Vault_Scavanger.Interfaces.CoreInterfaces;

namespace Vault_Scavanger.Classes.Core.InteractionFinder.InteractionRules;

public class DropSelectionRule : IInteractionRule
{
    
    public DropSelectionRule()
    {
    }

    public bool canInteract(Maze maze, Player player, InputMode inputMode)
    {
        return inputMode == InputMode.Drop && player.isDropPossible() != DropOptions.None;
    }

    public List<string> GetInteractions(Maze maze, Player player, InputMode inputMode, InteractionMessages interactionMessages)
    {
        List<string> result = new List<string>();
        if (canInteract(maze, player, inputMode))
        {
            DropOptions dropOptions = player.isDropPossible();
            if ((dropOptions & DropOptions.Item) != 0)
            {
                List<string> availableItemss = player.getAllItemNames();
                int i = 0;
                foreach (string availableItem in availableItemss)
                {
                    char c = (char)('0' + i);
                    result.Add(interactionMessages.SelectItemMessage(c, availableItem, inputMode));
                    i++;
                }

            }
            if ((dropOptions & DropOptions.BottleCap) != 0)
                result.Add(interactionMessages.SelectItemMessage(BottleCap.getChar, BottleCap.getName, inputMode));
            if ((dropOptions & DropOptions.GoldBar) != 0)
                result.Add(interactionMessages.SelectItemMessage(GoldBar.getChar, GoldBar.getName, inputMode));
        }
        return result;
    }
}
