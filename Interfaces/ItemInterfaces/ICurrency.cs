using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Classes.Core;

namespace Maze_Mania.Interfaces.ItemInterfaces;

public interface ICurrency : IItem
{
    void AddCurrency(Inventory inv);
}
