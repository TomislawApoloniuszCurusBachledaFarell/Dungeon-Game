using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Core;
using Vault_Scavanger.Enums;

namespace Vault_Scavanger.Classes.Utilis.Factories;

public static class EnemyFactory
{
    public static Enemy CreateRandomEnemy(Random rand, int y, int x)
    {
        int variant = rand.Next(2);
        Enemy result;
        switch (variant)
        {
            case 0:
                result = new Enemy(y, x) { Name = "Deathclaw", Symbol = 'D', Stats = new StatManager() };
                result.Stats.ImplementStat(StatType.health, 500, 500);
                result.Stats.ImplementStat(StatType.strength, 10, 9, false);
                result.Stats.ImplementStat(StatType.luck, 10, 7, false);
                result.Stats.ImplementStat(StatType.agility, 10, 8, false);
                result.Stats.ImplementStat(StatType.perception, 10, 7, false);
                result.Stats.ImplementStat(StatType.inteligence, 10, 4, false);
                result.Stats.ImplementStat(StatType.armour, 100, 15, false);
                result.Stats.ImplementStat(StatType.baseDmg, 250, 10, false);
                break;
            default:
                result = new Enemy(y, x) { Name = "Mole Rat", Symbol = 'r', Stats = new StatManager() };
                result.Stats.ImplementStat(StatType.health, 35, 35);
                result.Stats.ImplementStat(StatType.strength, 10, 2);
                result.Stats.ImplementStat(StatType.luck, 10, 4);
                result.Stats.ImplementStat(StatType.agility, 10, 4, false);
                result.Stats.ImplementStat(StatType.perception, 10, 3, false);
                result.Stats.ImplementStat(StatType.inteligence, 10, 1, false);
                result.Stats.ImplementStat(StatType.armour, 100, 1, false);
                result.Stats.ImplementStat(StatType.baseDmg, 20, 10, false);
                break;
        };
        return result;
        
    }
}
