using Maze_Mania.Classes.Core;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Enums;
using Maze_Mania.Interfaces.ItemInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Items.Equipable;

namespace Maze_Mania.Classes.Items.Weapon;

public abstract class Weapon : BaseEquipable
{
    public int Damage { get; set; }
    protected Weapon(string name, char symbol, int value, bool twoHanded, int damage) :
        base(name, symbol, value, twoHanded)
    {
        Damage = damage;
    }
}
