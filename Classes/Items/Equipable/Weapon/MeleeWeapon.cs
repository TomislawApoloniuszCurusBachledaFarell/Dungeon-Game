using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Classes.Items.Weapon;
using Maze_Mania.Interfaces;

public class MeleeWeapon : Weapon
{

    public MeleeWeapon(string name, char symbol, int value, bool twoHanded, int damage)
        : base(name, symbol, value, twoHanded, damage)
    {

    }
}
