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
public class UpperLimbsAttack : IAttackType
{
    public string Name { get => "target enemy's upper limbs"; }
    public string Visit(MeleeWeapon weapon, ITarget attacker, ITarget defender)
    {
        int dmg = (int)(weapon.Damage * 0.8);
        int arm = defender.Stats.GetStatValue(StatType.armour);

        int AAgl = attacker.Stats.GetStatValue(StatType.agility);
        int AStr = attacker.Stats.GetStatValue(StatType.strength);
        int DStr = defender.Stats.GetStatValue(StatType.strength);
        int DAgl = defender.Stats.GetStatValue(StatType.agility);

        int DodgeChance = (int)(DAgl * 6 - AAgl * 2.5);
        int CritChance = (int)(AStr * 5 - DStr * 2.5);
        string result;
        Random rand = new Random();

        if (rand.Next(100) < DodgeChance)
        {
            dmg = 0;

            result = CombatMessage.DodgedAttack(attacker.Name);
        }
        else if (rand.Next(100) < CritChance)
        {
            StatType StatNerfed = StatType.baseDmg;
            int duration = dmg;
            int nerfVal = defender.Stats.GetStatValue(StatNerfed) / 2;
            defender.Stats.AddEffect(new List<Effect>() { Effect.GetNegativeEffect(StatNerfed, nerfVal) },
            "BrokenArm", duration);

            dmg = (int)(dmg * 1.2);
            dmg = Math.Max(0, dmg - arm);

            result = CombatMessage.CriticalStrike(attacker.Name, dmg);
            result = $"{result}. {CombatMessage.BrokenAttack(StatNerfed, nerfVal)}";
        }
        else
        {
            dmg = Math.Max(0, dmg - arm);

            result = CombatMessage.NormalAttack(attacker.Name, dmg);

        }
        defender.TakeDamage(dmg);
        return result;
    }

    public string Visit(FirearmWeapon weapon, ITarget attacker, ITarget defender)
    {
        int dmg = (int) (weapon.Damage * 0.8);
        int arm = defender.Stats.GetStatValue(StatType.armour);

        int ALck = attacker.Stats.GetStatValue(StatType.luck);
        int APer = attacker.Stats.GetStatValue(StatType.perception);
        int DAgl = defender.Stats.GetStatValue(StatType.agility);
        int DLck = defender.Stats.GetStatValue(StatType.luck);

        int MissChance = (int)(DAgl * 6 - APer * 2.5);
        int CrippleChance = (int)(ALck * 5 - DLck * 2.5);
        string result;
        Random rand = new Random();

        if (rand.Next(100) < MissChance)
        {
            dmg = 0;
            result = CombatMessage.MissedShot(attacker.Name);
        }
        else if (rand.Next(100) < CrippleChance)
        {
            dmg = (int)(1.2 * dmg);
            dmg = Math.Max(0, dmg - arm);

            StatType StatNerfed = StatType.strength;

            int nerfVal = Math.Max(defender.Stats.GetStatValue(StatNerfed)/2, 3); 
            defender.Stats.AddEffect(new List<Effect>() { Effect.GetNegativeEffect(StatNerfed, nerfVal) },
            "CrippledArm", 2);
            result = CombatMessage.CripplingAttack(attacker.Name, dmg, StatNerfed, nerfVal);
        }
        else
        {
            dmg = Math.Max(0, dmg - arm);
            result = CombatMessage.NormalAttack(attacker.Name, dmg);
        }

        defender.TakeDamage(dmg);
        return result;
    }

    public string Visit(BaseEquipable weapon, ITarget attacker, ITarget defender)
    {
        int dmg = (int)(attacker.Stats.GetStatValue(StatType.baseDmg) * 0.8);
        int arm = defender.Stats.GetStatValue(StatType.armour);

        int ALck = attacker.Stats.GetStatValue(StatType.luck);
        int AStr = attacker.Stats.GetStatValue(StatType.strength);
        int DStr = defender.Stats.GetStatValue(StatType.strength);
        int DLck = defender.Stats.GetStatValue(StatType.luck);

        int CritChance = (int)(ALck * 5 - DLck * 2.5);
        int BlockChance = (int)(DStr * 7.5 - AStr * 2.5);
        string result;
        Random rand = new Random();

        if (rand.Next(100) < BlockChance)
        {
            dmg = (int)(dmg * 0.5);
            dmg = Math.Max(0, dmg - arm);

            result = CombatMessage.BlockedAnAttack(attacker.Name, defender.Name, dmg);
        }
        else if (rand.Next(100) < CritChance)
        {
            StatType StatBuffed = StatType.baseDmg;
            int buffVal = dmg;
            attacker.Stats.AddEffect(new List<Effect>() { Effect.GetPositiveEffect(StatBuffed, buffVal) },
            "EnpoweredAttack", 2);

            dmg = (int)(dmg * 1.25);
            dmg = Math.Max(0, dmg - arm);

            result = CombatMessage.CriticalStrike(attacker.Name, dmg);
            result = $"{result}. {CombatMessage.EmpoweredNextAttack(attacker.Name, dmg)}";
        }
        else
        {
            dmg = Math.Max(0, dmg - arm);

            result = CombatMessage.NormalAttack(attacker.Name, dmg);

        }
        defender.TakeDamage(dmg);
        return result;
    }
    public string Visit(EnergyWeapon weapon, ITarget attacker, ITarget defender)
    {
        int dmg = (int)(weapon.Damage * 0.8);

        int ALck = attacker.Stats.GetStatValue(StatType.luck);
        int AInt = attacker.Stats.GetStatValue(StatType.inteligence);
        int DPer = defender.Stats.GetStatValue(StatType.perception);
        int DLck = defender.Stats.GetStatValue(StatType.luck);

        int MissChance = (int)(DPer * 5 - AInt * 2.5);
        int BurnChance = (int)(ALck * 5 - DLck * 2.5);
        string result;
        Random rand = new Random();
        if (rand.Next(100) < MissChance)
        {
            dmg = 0;
            result = CombatMessage.MissedShot(attacker.Name);
        }
        else if (rand.Next(100) < BurnChance)
        {
            int targetHP = defender.Stats.GetStatMaxValue(StatType.health);
            StatType StatNerfed = StatType.health;
            int nerfVal = Math.Max((int)((AInt * 0.01) * targetHP), 1);

            defender.Stats.AddTickEffect(new List<Effect>() { Effect.GetNegativeEffect(StatNerfed, nerfVal) },
            "LaserBurn", 5);

            dmg = (int)(1.25 * dmg);

            result = CombatMessage.CriticalStrike(attacker.Name, dmg);
            result = $"{result}. {CombatMessage.BurnAttack()}";
        }
        else
        {
            result = CombatMessage.EnergyAttack(attacker.Name, dmg);
        }

        defender.TakeDamage(dmg);
        return result;
    }
}
