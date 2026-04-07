using Maze_Mania.Classes.Core;
using Maze_Mania.Enums;
using Maze_Mania.Interfaces.CoreInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Core.AttackType;
using Vault_Scavanger.Classes.Utilis.Messages;
using Vault_Scavanger.Interfaces.CoreInterfaces;

namespace Vault_Scavanger.Classes.Core.InteractionFinder.InteractionRules;

public class AttackHandSelectionRule : IInteractionRule
{
    public  AttackHandSelectionRule()
    {
    }

    public bool canInteract(Maze maze, Player player, InputMode inputMode)
    {
        return (inputMode == InputMode.AttackHandSelection);
    }


    public List<string> GetInteractions(Maze maze, Player player, InputMode inputMode, InteractionMessages interactionMessages)
    {
        List<string> result = new List<string>();
        if (canInteract(maze, player, inputMode))
        {

            result.Add(interactionMessages.LeftHandMessage(inputMode));
            result.Add(interactionMessages.RightHandMessage(inputMode));

        }
        return result;
    }
}
