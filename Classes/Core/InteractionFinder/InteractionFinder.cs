using Maze_Mania.Classes.Core;
using Maze_Mania.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Core.InteractionFinder.InteractionRules;
using Vault_Scavanger.Classes.Utilis.Messages;
using Vault_Scavanger.Interfaces.CoreInterfaces;

namespace Vault_Scavanger.Classes.Core.InteractionsBuilder;

public class InteractionFinder
{
    List<IInteractionRule> interactions;
    InteractionMessages interactionMessages;

    public InteractionFinder(KeyDefinitions keys) 
    {
        interactions = new List<IInteractionRule>();

        interactions.Add(new PickUpRule());
        interactions.Add(new CombatRule());
        interactions.Add(new EquipRule());
        interactions.Add(new DropRule());
        interactions.Add(new UnequipRule());
        interactions.Add(new DropSelectionRule());
        interactions.Add(new EquipSelectionRule());
        interactions.Add(new EquipHandSelectionRule());
        interactions.Add(new AttackHandSelectionRule());
        interactions.Add(new UnequipHandSelectionRule());
        interactions.Add(new CancelRule());
        interactions.Add(new ExitGameRule());

        interactionMessages = new InteractionMessages(keys);
    }

    public List<string> FindInteractions(Maze maze, Player player, InputMode inputMode)
    {
        List<string> result = new List<string>();
        foreach(IInteractionRule rule in interactions)
        {
            result.AddRange(rule.GetInteractions(maze, player, inputMode, interactionMessages));
        }
        return result;
    }

}
