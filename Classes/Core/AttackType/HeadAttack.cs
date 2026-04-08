using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Items.Equipable;
using Vault_Scavanger.Classes.Items.Equipable.Weapon;
using Vault_Scavanger.Classes.Utilis;
using Vault_Scavanger.Classes.Utilis.Messages;
using Vault_Scavanger.Enums;
using Vault_Scavanger.Interfaces.CoreInterfaces;

namespace Vault_Scavanger.Classes.Core.AttackType;
public class HeadAttack : IAttackType
{
    public string Name { get => "target enemy's head"; }
    public string Visit(MeleeWeapon weapon, ITarget attacker, ITarget defender)
    {
        int dmg = (int)(1.5 * weapon.Damage);
        int arm = defender.Stats.GetStatValue(StatType.armour);

        int AInt = attacker.Stats.GetStatValue(StatType.inteligence);
        int AStr = attacker.Stats.GetStatValue(StatType.strength);
        int DStr = defender.Stats.GetStatValue(StatType.strength);
        int DAgl = defender.Stats.GetStatValue(StatType.agility);

        int WhiffChance = (int)(6 * DAgl - 2 * AInt);
        int BleedChance = (int)(AStr * 6 - DStr * 2);

        string result;
        Random rand = new Random();

        if (rand.Next(100) < WhiffChance)
        {
            dmg = 0;

            result = CombatMessage.DodgedAttack(attacker.Name);
        }
        else
        {
            dmg = Math.Max(0, dmg - arm);

            result = CombatMessage.NormalAttack(attacker.Name, dmg);
            if (rand.Next(100) < BleedChance)
            {
                int healthDebuff = Math.Max(1, (AStr + dmg)/5);
                StatType statDebuffed = StatType.health;
                defender.Stats.AddTickEffect(new List<Effect>() { Effect.GetNegativeEffect(statDebuffed, healthDebuff) },
                    "Bleed", 5);

                result = result + ". " + CombatMessage.TargetBleeding();
            }
        }


        defender.TakeDamage(dmg);
        return result;
    }
    public string Visit(FirearmWeapon weapon, ITarget attacker, ITarget defender)
    {
        int dmg = 2 * weapon.Damage;
        int arm = defender.Stats.GetStatValue(StatType.armour);

        int ALck = attacker.Stats.GetStatValue(StatType.luck);
        int APer = attacker.Stats.GetStatValue(StatType.perception);
        int DAgl = defender.Stats.GetStatValue(StatType.agility);
        int DLck = defender.Stats.GetStatValue(StatType.luck);

        int MissChance = (int)((DAgl+DAgl) * 5 - (APer + ALck) * 2.5);
        int RicochetChance = arm/2 - dmg;
        string result;
        Random rand = new Random();

        if (rand.Next(100) < MissChance)
        {
            dmg = 0;
            result = CombatMessage.MissedShot(attacker.Name);
        }
        else if (rand.Next(100) < RicochetChance)
        {
            dmg = 0;
            int armourReduction = (int)Math.Ceiling(arm * 0.1);
            defender.Stats.ModifyStat(StatType.armour, (-1) * armourReduction);

            result = CombatMessage.RicochetShot(attacker.Name, armourReduction);
        }
        else
        {
            dmg = Math.Max(0, dmg - arm);
            result = CombatMessage.Headshot(attacker.Name, dmg);
        }
        defender.TakeDamage(dmg);
        return result;
    }
    public string Visit(EnergyWeapon weapon, ITarget attacker, ITarget defender)
    {
        int dmg =(int)(weapon.Damage * 1.75);

        int ALck = attacker.Stats.GetStatValue(StatType.luck);
        int AInt = attacker.Stats.GetStatValue(StatType.inteligence);
        int DPer = defender.Stats.GetStatValue(StatType.perception);
        int DInt = defender.Stats.GetStatValue(StatType.inteligence);
        int DLck = defender.Stats.GetStatValue(StatType.luck);

        int MissChance = (int)((DPer + DInt) * 5 - AInt * 2.5);
        int SynnapseBurnChance = (int)(ALck * 5 - DLck * 2.5);
        string result;
        Random rand = new Random();
        if (rand.Next(100) < MissChance)
        {
            dmg = 0;
            result = CombatMessage.MissedShot(attacker.Name);
        }
        else
        {
            result = CombatMessage.EnergyAttack(attacker.Name, dmg);
            if (rand.Next(100) < SynnapseBurnChance)
            {
                StatType NerfedStat1 = StatType.inteligence;
                StatType NerfedStat2 = StatType.luck;
                int nerfVal = 3;

                defender.Stats.AddEffect(new List<Effect>() { Effect.GetNegativeEffect(NerfedStat1, nerfVal),
                Effect.GetNegativeEffect(NerfedStat2, nerfVal)},
                    "SynnapseBurnt", 3);
                result = $"{result}. {CombatMessage.MeltCrit(attacker.Name, dmg)}";
            }
        }
        defender.TakeDamage(dmg);
        return result;
    }
    public string Visit(BaseEquipable weapon, ITarget attacker, ITarget defender)
    {
        int dmg =(int)(attacker.Stats.GetStatValue(StatType.baseDmg) * 1.5);
        int arm = defender.Stats.GetStatValue(StatType.armour);

        int AAgl = attacker.Stats.GetStatValue(StatType.agility);
        int AStr = attacker.Stats.GetStatValue(StatType.strength);
        int DStr = defender.Stats.GetStatValue(StatType.strength);
        int DInt = defender.Stats.GetStatValue(StatType.inteligence);

        int BlockChance = (int)((DStr + DInt) * 5 - (AStr + AAgl) * 2.5);
        string result;
        Random rand = new Random();

        if (rand.Next(100) < BlockChance)
        {
            dmg = (int)(dmg * 0.125);
            dmg = Math.Max(0, dmg - arm);

            result = CombatMessage.BlockedAnAttack(attacker.Name, defender.Name, dmg);
        }
        else
        {
            dmg = Math.Max(0, dmg - arm);

            result = CombatMessage.NormalAttack(attacker.Name, dmg);

        }
        defender.TakeDamage(dmg);
        return result;
    }
}
