using Maze_Mania.Classes.Utilis;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Core;
using Vault_Scavanger.Enums;

namespace Vault_Scavanger.Classes.Utilis.Messages;

public static class CombatMessage
{
    public static string CriticalStrike(string attackerName, int dmg) =>
        $"{attackerName} performed a critical strike dealing {dmg} damage";
    public static string NormalAttack(string attackerName, int dmg) =>
        $"{attackerName} dealt {dmg} damage";
    public static string BlockedAnAttack(string attackerName, string defenderName, int dmg) =>
        $"{attackerName} striked but {defenderName} blocked reducing the damage to {dmg}";
    public static string MissedShot(string attackerName) =>
        $"{attackerName} shot but completely missed the target dealing no damage";
    public static string RicochetShot(string attackerName, int armourReduction) =>
        $"{attackerName} hit but the bullet ricocheted dealing no damage to the target but reducing its armour by {armourReduction}";
    public static string PuncturedShot(string attackerName, int dmg, int armourReduction) =>
        $"{attackerName} dealt {dmg} damage and punctured target's armour reducing it by {armourReduction}";
    public static string TargetStunned(string attackerName, StatType type, int reduction) =>
        $"{attackerName} stunned the target reducing its {Stats.GetStatTypeName(type)} by {reduction}";
    public static string MeltCrit(string attackerName, int dmg) =>
        $"{attackerName} performed a critical hit melting target's defence dealing {dmg} damage";
    public static string EnergyAttack(string attackerName, int dmg) =>
        $"{attackerName} dealt {dmg} damage with an energy weapon bypassing amour";
    public static string EmpoweredNextAttack(string attackerNme, int dmg) =>
        $"Next attack of will be empowered";
    public static string CripplingAttack(string attackerName, int dmg, StatType type, int reduction) =>
        $"{attackerName} crippled the target reducing its {Stats.GetStatTypeName(type)} by {reduction}";
    public static string DodgedAttack(string attackerName) =>
        $"{attackerName} attacked but the target dodge, no damage was dealt";
    public static string BrokenAttack(StatType type, int reduction) =>
        $"Target's arm was broken reducing its {Stats.GetStatTypeName(type)} by {reduction}";
    public static string BurnAttack() =>
        $"Target was set on fire";
    public static string TargetBleeding() =>
        $"Target is bleeding";
    public static string SynnapsesBurt(StatType type1, StatType type2) =>
        $"Target's synnapses were burnt reducing its {type1} and {type2}";
    public static string Headshot(string attackerName, int dmg) =>
        $"{attackerName} hit a headshot dealing {dmg} damage";
}
