using Maze_Mania.Classes.Utilis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Core;

namespace Vault_Scavanger.Interfaces.CoreInterfaces;

public interface ITarget
{
    public StatManager Stats { get; set; }
    public string Name { get; }
    public void TakeDamage(int dmg);
}
