using Maze_Mania.Classes.Core;
using Maze_Mania.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Utilis;
using Vault_Scavanger.Interfaces.CoreInterfaces;

namespace Vault_Scavanger.Classes.Core.InteractionFinder.InteractionRules;

public class EquipSelectionRule : IInteractionRule
{

    public EquipSelectionRule()
    {
    }

    public bool canInteract(Maze maze, Player player, InputMode inputMode)
    {
        return (inputMode == InputMode.Equip);
    }


    public List<string> GetInteractions(Maze maze, Player player, InputMode inputMode, InteractionMessages interactionMessages)
    {
        List<string> result = new List<string>();
        if (canInteract(maze, player, inputMode))
        {
            List<bool> CanSelect = player.getAllItemsSelectability();
            List<string> itemNames = player.getAllItemNames();
            for(int i = 0; i <itemNames.Count; i++)
            {
                if (!CanSelect[i])
                    continue;
                char c = (char)('0' + i);
                result.Add(interactionMessages.SelectItemMessage(c, itemNames[i], inputMode));
            }
        }
        return result;
    }
}
