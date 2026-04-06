using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Items.Equipable;
using Vault_Scavanger.Classes.Items.Equipable.Weapon;

namespace Vault_Scavanger.Interfaces.CoreInterfaces;

public interface IAttackType
{
    public string Name { get; }
    public int Visit(MeleeWeapon weapon, ITarget Attcker, ITarget Defender);
    public int Visit(RangedWeapon weapon, ITarget Attacker, ITarget Defender);
    public int Visit(BaseEquipable weapon, ITarget Attacker, ITarget Defender);
}