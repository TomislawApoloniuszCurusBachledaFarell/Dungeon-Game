using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Interfaces.ItemInterfaces;
using Vault_Scavanger.Interfaces.ItemInterfaces;

namespace Maze_Mania.Classes.Utilis;

public class Hands
{
    public IEquipable?[] itemSlot;
    public bool[] isOccupied;
    public Hands()
    {
        itemSlot = new IEquipable?[2];
        isOccupied = [false, false];
    }
}
