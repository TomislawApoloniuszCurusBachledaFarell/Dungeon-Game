using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Interfaces.CoreInterfaces;

namespace Vault_Scavanger.Classes.Core.AttackType;

public static class AttackTypeManager
{
    public static List<IAttackType> AttackTypes = new List<IAttackType>()
    {
        new MeleeAttack(),
        new RangedAttack(),
    };

    public static IAttackType GetAttack(int index)
    {
        return AttackTypes[index];  
    }
}
