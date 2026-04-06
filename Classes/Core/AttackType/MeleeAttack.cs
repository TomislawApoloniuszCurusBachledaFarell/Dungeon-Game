using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Items.Equipable;
using Vault_Scavanger.Classes.Items.Equipable.Weapon;
using Vault_Scavanger.Interfaces.CoreInterfaces;

namespace Vault_Scavanger.Classes.Core.AttackType;

public class MeleeAttack : IAttackType
{
    public string Name { get => "Melee Attack"; }
    public int Visit(MeleeWeapon weapon, ITarget attacker, ITarget defender)
    {
        return 0;
    }

    public int Visit(RangedWeapon weapon, ITarget attacker, ITarget defender)
    {
        return 0;
    }

    public int Visit(BaseEquipable weapon, ITarget attacker, ITarget defender)
    {
        return 0;
    }
}
