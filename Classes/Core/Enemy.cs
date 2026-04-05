using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vault_Scavanger.Classes.Core;

public class Enemy
{
    public string Name;
    public char Symbol;
    public StatManager Stats;
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
}
