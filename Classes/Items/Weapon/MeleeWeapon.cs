using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Interfaces;

namespace Maze_Mania.Classes.Items.Weapon;

internal class MeleeWeapon : Weapon
{

    public MeleeWeapon(string name, char symbol, int value, int damage, bool twoHanded)
        : base(name, symbol, value, damage, twoHanded)
    {

    }
}
