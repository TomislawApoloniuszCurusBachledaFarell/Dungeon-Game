using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Classes.Core;

namespace Maze_Mania.Interfaces.ItemInterfaces;

public interface IItem
{
    public string Name { get; }
    public char Symbol { get; }
    public void PickUp(Inventory inv);

}

