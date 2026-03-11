using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Interfaces;

namespace Maze_Mania.Classes.Utilis;

public class Hands
{
    public IEquiplable?[] itemSlot;
    public bool[] isOccupied;
    public Hands()
    {
        itemSlot = new IEquiplable?[2];
        isOccupied = [false, false];
    }
}
