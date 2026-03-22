using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Classes;

namespace Maze_Mania.Interfaces.ItemInterfaces;

public interface IEquiplable : IItem
{
    public int Value { get; }
    bool TwoHanded { get; }
}
