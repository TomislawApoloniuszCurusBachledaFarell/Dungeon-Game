using Maze_Mania.Classes.Core;
using Maze_Mania.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Core.AttackType;
using Vault_Scavanger.Classes.Utilis;
using Vault_Scavanger.Interfaces.CoreInterfaces;

namespace Vault_Scavanger.Classes.Core.InteractionFinder.InteractionRules;

public class CombatRule : IInteractionRule
{
    public CombatRule()
    {
    }

    public bool canInteract(Maze maze, Player player, InputMode inputMode)
    {
        return (inputMode == InputMode.Combat);
    }

    public List<string> GetInteractions(Maze maze, Player player, InputMode inputMode, InteractionMessages interactionMessages)
    {
        List<string> result = new List<string>();
        if (canInteract(maze, player, inputMode))
        {
            List<IAttackType> attackTypes = AttackTypeManager.AttackTypes;
            char ckey = '0';
            foreach (IAttackType attackType in attackTypes)
            {
                result.Add(interactionMessages.SelectAttackType(ckey, attackType.Name));
                ckey++;
            }
        }
        return result;
    }
}
