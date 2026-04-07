using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Items.Equipable;
using Vault_Scavanger.Classes.Items.Equipable.EquipableDecorator;
using Vault_Scavanger.Classes.Items.Equipable.Weapon;
using Vault_Scavanger.Classes.Items.Equipable.Weapon.WeaponDecorators;

namespace Vault_Scavanger.Classes.Utilis.Factories;

public static class WeaponFactory
{
    public static BaseEquipable CreateRandomWeapon(Random rand)
    {
        int variant = rand.Next(4);
        Weapon result = variant switch
        {
            0 => new FirearmWeapon("BB Gun", 'g', 36, true, 4),
            1 => new FirearmWeapon("10mm Pistol", '¬', 250, false, 22),
            2 => new EnergyWeapon("Laser Pistol", 'l', 175, false, 12),
            _ => new MeleeWeapon("Rolling Pin", 'R', 10, false, 3),
        };

        if (rand.Next(5) == 0)
            result = new RustyDecorator(result);

        BaseEquipable decoratedResult = result;
        if (rand.Next(5) == 0)
            decoratedResult = new GamblersDecorator(decoratedResult);
        if (rand.Next(5) == 0)
            decoratedResult = new LifegiverDecorator(decoratedResult);

        return decoratedResult;
    }
}
