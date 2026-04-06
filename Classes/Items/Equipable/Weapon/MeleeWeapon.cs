using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Vault_Scavanger.Classes.Items.Equipable.Weapon;

using Maze_Mania.Interfaces;
using Vault_Scavanger.Interfaces.CoreInterfaces;

public class MeleeWeapon : Weapon
{

    public MeleeWeapon(string name, char symbol, int value, bool twoHanded, int damage)
        : base(name, symbol, value, twoHanded, damage)
    {

    }

    public override int Accept(IAttackType attack, ITarget attacker, ITarget defender)
    {
        return attack.Visit(this, attacker, defender);
    }
}
