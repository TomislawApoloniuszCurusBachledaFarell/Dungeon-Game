using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Items.Equipable;
using Vault_Scavanger.Classes.Items.Equipable.Weapon;
using Vault_Scavanger.Interfaces.CoreInterfaces;

namespace Vault_Scavanger.Classes.Core.AttackType;

public class RangedAttack : IAttackType
{
    public string Name { get => "Ranged Attack"; }
    public int Visit(MeleeWeapon weapon, ITarget attacker, ITarget defender)
    {
        return 100;
    }

    public int Visit(RangedWeapon weapon, ITarget attacker, ITarget defender)
    {
        return 100;
    }

    public int Visit(BaseEquipable weapon, ITarget attacker, ITarget defender)
    {
        return 100;
    }
}
