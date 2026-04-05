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
    public override int Value => (int)(base.Value * (0.5));
    public override int Damage => Math.Max(1,(int)( base.Damage - (0.33) * base.Damage));
}
