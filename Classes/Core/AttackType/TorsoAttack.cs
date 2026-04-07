using Maze_Mania.Classes.Core;
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

public class TorsoAttack : IAttackType
{
    public string Name { get => "target enemy's torso"; }
    public string Visit(MeleeWeapon weapon, ITarget attacker, ITarget defender)
    {
        int dmg = weapon.Damage;
        int arm = defender.Stats.GetStatValue(StatType.armour);

        int ALck = attacker.Stats.GetStatValue(StatType.luck);
        int AStr = attacker.Stats.GetStatValue(StatType.strength);
        int DStr = defender.Stats.GetStatValue(StatType.strength);
        int DLck = defender.Stats.GetStatValue(StatType.luck);

        int CriticalChance = (int)(ALck * 5 - ALck * 2.5);
        int StunChance = (int)(ALck * 5 - DLck * 2.5);
        int RicochetChance = arm - dmg;
        string result;
        Random rand = new Random();

        if (rand.Next(100) < CriticalChance)
        {
            dmg = (int)(1.2 * dmg);
            dmg = Math.Max(0, dmg - arm);

            result = CombatMessage.CriticalStrike(attacker.Name, dmg);
        }
        else
        {
            dmg = Math.Max(0, dmg - arm);

            result = CombatMessage.NormalAttack(attacker.Name, dmg);
        }
        if (rand.Next(100) < StunChance)
        {
            int agilityDebuff = 4;
            StatType statDebuffed = StatType.agility;
            defender.Stats.AddEffect(new List<Effect>() { Effect.GetNegativeEffect(statDebuffed, agilityDebuff) },
                "MeleeStun", 2);

            result = result + ". " + CombatMessage.TargetStunned(attacker.Name, statDebuffed, agilityDebuff);
        }

        defender.TakeDamage(dmg);
        return result;
    }

    public string Visit(FirearmWeapon weapon, ITarget attacker, ITarget defender)
    {
        int dmg = weapon.Damage;
        int arm = defender.Stats.GetStatValue(StatType.armour);

        int ALck = attacker.Stats.GetStatValue(StatType.luck);
        int APer = attacker.Stats.GetStatValue(StatType.perception);
        int DAgl = defender.Stats.GetStatValue(StatType.agility);
        int DLck = defender.Stats.GetStatValue(StatType.luck);

        int MissChance = (int)(DAgl * 5 - APer * 2.5);
        int PunctureChance = (int)(ALck * 5 - DLck * 2.5);
        int RicochetChance = arm - dmg;
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
            int armourReduction = (int)Math.Ceiling(arm * 0.05);
            defender.Stats.ModifyStat(StatType.armour, (-1) * armourReduction);

            result = CombatMessage.RicochetShot(attacker.Name, armourReduction);
        }
        else if (rand.Next(100) < PunctureChance)
        {
            dmg = (int)(1.25 * dmg);
            int armourReduction = (int)Math.Ceiling(arm * 0.2);
            defender.Stats.ModifyStat(StatType.armour, (-1) * armourReduction);
            result = CombatMessage.PuncturedShot(attacker.Name, dmg, armourReduction);
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
        int dmg = weapon.Damage;
        int arm = defender.Stats.GetStatValue(StatType.armour);

        int ALck = attacker.Stats.GetStatValue(StatType.luck);
        int AInt = attacker.Stats.GetStatValue(StatType.inteligence);
        int DPer = defender.Stats.GetStatValue(StatType.perception);
        int DLck = defender.Stats.GetStatValue(StatType.luck);

        int MissChance = (int)(DPer * 5 - AInt * 2.5);
        int MeltChance = (int)(ALck * 5 - DLck * 2.5);
        string result;
        Random rand = new Random();

        if (rand.Next(100) < MissChance)
        {
            dmg = 0;
            result = CombatMessage.MissedShot(attacker.Name);
        }
        else if (rand.Next(100) < MeltChance)
        {
            dmg += (int)(0.25 * (AInt * 0.1));

            result = CombatMessage.MeltCrit(attacker.Name, dmg);
        }
        else
        {
            result = CombatMessage.EnergyAttack(attacker.Name, dmg);
        }

        defender.TakeDamage(dmg);
        return result;
    }

    public string Visit(BaseEquipable weapon, ITarget attacker, ITarget defender)
    {
        int dmg = attacker.Stats.GetStatValue(StatType.baseDmg);
        int arm = defender.Stats.GetStatValue(StatType.armour);

        int ALck = attacker.Stats.GetStatValue(StatType.luck);
        int AStr = attacker.Stats.GetStatValue(StatType.strength);
        int DStr = defender.Stats.GetStatValue(StatType.strength);
        int DLck = defender.Stats.GetStatValue(StatType.luck);

        int CritChance = (int)(ALck * 5 - DLck * 2.5);
        int BlockChance = (int)(DStr * 5 - AStr * 2.5);
        string result;
        Random rand = new Random();

        if (rand.Next(100) < CritChance)
        {
            dmg = (int)(dmg * 1.5);
            dmg = Math.Max(0, dmg - arm);
            
            result = CombatMessage.CriticalStrike(attacker.Name, dmg);
        }
        else if (rand.Next(100) < BlockChance)
        {
            dmg = (int)(dmg * 0.5);
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
