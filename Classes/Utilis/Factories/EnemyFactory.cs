using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Core;

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
                result.Stats.AddHealth(35);
                result.Stats.AddStrength(2);
                result.Stats.AddLuck(4);
                result.Stats.AddArmour(1);
                break;
        };
        return result;
        
    }
}
