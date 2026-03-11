using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Classes.Core;
using Maze_Mania.Interfaces;

namespace Maze_Mania.Classes.Items.Miscellaneous;

public class MiscellaneousItem : IEquiplable
{
    public string Name { get; set; }
    public char Symbol { get; set; }
    public int Value { get; set; }
    public bool TwoHanded { get; set; } = false;
    public void PickUp(Inventory inv) => inv.AddItem(this);
}
