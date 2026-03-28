using Maze_Mania.Classes.Core;
using Maze_Mania.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Interfaces.CoreInterfaces;

namespace Vault_Scavanger.Classes.Core.InteractionFinder.InteractionRules;

public class UnequipRule : IInteractionRule
{
    public UnequipRule()
    {
    }

    public bool canInteract(Maze maze, Player player, InputMode inputMode)
    {
        return (inputMode == InputMode.Normal && (player.isLeftHandOccupied() || player.isRightHandOccupied()) && player.hasInventorySpace());
    }


    public List<string> GetInteractions(Maze maze, Player player, InputMode inputMode, InteractionMessages interactionMessages)
    {
        List<string> result = new List<string>();
        if (canInteract(maze, player, inputMode))
        {
            result.Add(interactionMessages.UnequipMessage());
        }
        return result;
    }
}
