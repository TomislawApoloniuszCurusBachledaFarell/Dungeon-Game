using Vault_Scavanger.Classes.Items.Equipable.Weapon;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vault_Scavanger.Classes.Items.Equipable.Weapon.WeaponDecorators;

public abstract class WeaponDecorator : Weapon
{
    protected Weapon baseWeapon;

    public WeaponDecorator(Weapon baseWeapon):
        base(baseWeapon.Name, baseWeapon.Symbol, baseWeapon.Value, baseWeapon.TwoHanded, baseWeapon.Damage)
    {
        this.baseWeapon = baseWeapon;
    }

    public override string Name => baseWeapon.Name;
    public override int Value => baseWeapon.Value;
    public virtual int Damage => baseWeapon.Damage;

}
