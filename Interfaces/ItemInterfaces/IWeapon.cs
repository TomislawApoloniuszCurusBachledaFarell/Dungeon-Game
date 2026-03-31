using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Interfaces.ItemInterfaces;

namespace Maze_Mania.Interfaces.ItemInterfaces;

public interface IWeapon : IEquipable
{
    int Damage { get; }
}
