using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vault_Scavanger.Classes.Items.Equipable.Weapon.WeaponDecorators;

public class RustyDecorator : WeaponDecorator
{
    public RustyDecorator(Weapon baseWeapon) : base(baseWeapon)
    {
    }
    public override string Name => $"Rusty {base.Name}";
    public override int Value => base.Value * (1/2);
    public override int Damage => Math.Max(1, base.Damage -(1/3) * base.Damage);
}
