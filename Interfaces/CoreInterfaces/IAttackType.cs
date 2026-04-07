using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Items.Equipable;
using Vault_Scavanger.Classes.Items.Equipable.Weapon;
using Vault_Scavanger.Classes.Utilis;

namespace Vault_Scavanger.Interfaces.CoreInterfaces;

public interface IAttackType
{
    public string Name { get; }
    public string Visit(MeleeWeapon weapon, ITarget Attcker, ITarget Defender);
    public string Visit(FirearmWeapon weapon, ITarget Attacker, ITarget Defender);
    public string Visit(BaseEquipable weapon, ITarget Attacker, ITarget Defender);
    public string Visit(EnergyWeapon weapon, ITarget attacker, ITarget defender);
}