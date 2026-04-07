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
        int variant = rand.Next(0);
        Enemy result;
        switch (variant)
        {
            case 0:
            default:
                result = new Enemy(y, x) { Name = "Mole Rat", Symbol = 'r', Stats = new StatManager() };
                result.Stats.ImplementStat(StatType.health, 35, 35);
                result.Stats.ImplementStat(StatType.strength, 10, 2);
                result.Stats.ImplementStat(StatType.luck, 10, 4);
                result.Stats.ImplementStat(StatType.agility, 10, 4, false);
                result.Stats.ImplementStat(StatType.perception, 10, 3, false);
                result.Stats.ImplementStat(StatType.inteligence, 10, 1, true);
                result.Stats.ImplementStat(StatType.armour, 100, 1, false);
                result.Stats.ImplementStat(StatType.baseDmg, 10, 10, false);
                break;
        };
        return result;
        
    }
}
