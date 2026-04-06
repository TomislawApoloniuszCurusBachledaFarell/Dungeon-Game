using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Interfaces.CoreInterfaces;

namespace Vault_Scavanger.Classes.Core;

public class Enemy : ITarget
{
    public string Name;
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
}
