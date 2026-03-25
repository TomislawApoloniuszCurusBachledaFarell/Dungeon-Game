using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Classes.Items.Miscellaneous;
using Maze_Mania.Classes.Items.Weapon;
using Maze_Mania.Interfaces.ItemInterfaces;
using Vault_Scavanger.Interfaces.CoreInterfaces;

namespace Vault_Scavanger.Classes.Core.VaultBuilder.Building_Procedures;


public class AddWeapons : IBuildProcedure
{
    int Count;
    List<IWeapon> PossibleWeapons;

    public AddWeapons(int count)
    {
        Count = count;
        PossibleWeapons = new List<IWeapon>()
        {
            new RangedWeapon ("BB Gun", 'g', 36, 4, true ),
            new RangedWeapon("10mm Pistol", 'P', 250, 22, false),
            new MeleeWeapon("Rolling Pin", 'R', 10, 3, false),
        };
    }

    public void Execute(VaultBuilder builder)
    {
        for (int i = 0; i < Count; i++)
        {
            int itemId = builder.rand.Next(PossibleWeapons.Count);
            IItem item = PossibleWeapons[itemId];
            List<(int Y, int X)> possibleTiles = builder.GetFreeSpaces();
            int tile = builder.rand.Next(possibleTiles.Count);
            if (possibleTiles.Count == 0) return;

            builder.addItem(item, possibleTiles[tile]);
        }
    }

}
