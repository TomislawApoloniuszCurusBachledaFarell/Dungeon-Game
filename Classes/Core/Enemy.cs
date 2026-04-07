using Maze_Mania.Classes.Utilis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Enums;
using Vault_Scavanger.Interfaces.CoreInterfaces;

namespace Vault_Scavanger.Classes.Core;

public class Enemy : ITarget
{
    public string Name { get; set; }
    public char Symbol;
    public StatManager Stats { get; set; }
    public int xPos { get; private set; }
    public int yPos { get; private set; }
    public Enemy(int yPos, int xPos)
    {
        Name = "";
        Symbol = '-';
        Stats = new StatManager();
        this.xPos = xPos;
        this.yPos = yPos;
    }

    public List<string> GetVisibleStats()
    {
        List<string> statsString = new List<string>();
        foreach (Stats stat in Stats.Stats.Values) 
        {
            string? statString = stat.ToString();
            if (statString != null)
                statsString.Add(statString);
        }
        return statsString;
    }

    public void TakeDamage(int dmg)
    {
        Stats.ModifyStat(StatType.health, (-1) * dmg);
    }

    public bool IsAlive() => Stats.GetStatValue(StatType.health) > 0;

}
