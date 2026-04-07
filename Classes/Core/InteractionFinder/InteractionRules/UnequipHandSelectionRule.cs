using Maze_Mania.Classes.Core;
using Maze_Mania.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Utilis.Messages;
using Vault_Scavanger.Interfaces.CoreInterfaces;

namespace Vault_Scavanger.Classes.Core.InteractionFinder.InteractionRules;

internal class UnequipHandSelectionRule : IInteractionRule
{

    public UnequipHandSelectionRule()
    {
    }

    public bool canInteract(Maze maze, Player player, InputMode inputMode)
    {
        return (inputMode == InputMode.HandUnequipSelection);
    }


    public List<string> GetInteractions(Maze maze, Player player, InputMode inputMode, InteractionMessages interactionMessages)
    {
        List<string> result = new List<string>();
        if (canInteract(maze, player, inputMode))
        {
            if(player.isLeftHandOccupied()) 
                result.Add(interactionMessages.LeftHandMessage(inputMode));
            if(player.isRightHandOccupied())
                result.Add(interactionMessages.RightHandMessage(inputMode));
        }
        return result;
    }
}
