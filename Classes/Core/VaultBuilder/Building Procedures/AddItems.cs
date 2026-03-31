using Maze_Mania.Classes.Items.Currency;
using Maze_Mania.Classes.Items.Miscellaneous;
using Maze_Mania.Classes.Items.Weapon;
using Maze_Mania.Interfaces.ItemInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Enums;
using Vault_Scavanger.Interfaces.CoreInterfaces;

namespace Vault_Scavanger.Classes.Core.VaultBuilder.Building_Procedures;

public class AddItems : IBuildProcedure
{
    int ItemCount;
    int WeaponCount;
    List<IItem> PossibleItems;
    List<IWeapon> PossibleWeapons;

    public AddItems(int ItemCount, int WeaponCount = 0)
    {
        this.ItemCount = ItemCount;
        PossibleItems = new List<IItem>()
        {
            new MiscellaneousItem { Name = "Empty Sunset Sarsaparilla bottle", Symbol = 'E', Value = 2 },
            new MiscellaneousItem { Name = "Big Empty Sunset Sarsaparilla bottle", Symbol = 'B', Value = 2, TwoHanded = true },
            new MiscellaneousItem { Name = "Bobby Pin", Symbol = 'B', Value = 0, TwoHanded = false },

        };

        this.WeaponCount = WeaponCount;
        PossibleWeapons = new List<IWeapon>()
        {
            new RangedWeapon ("BB Gun", 'g', 36, 4, true ),
            new RangedWeapon("10mm Pistol", '¬', 250, 22, false),
            new MeleeWeapon("Rolling Pin", 'R', 10, 3, false),
        };
    }

    public void Execute(VaultBuilder builder, ref Features features)
    {
        List<(int Y, int X)> possibleTiles = builder.GetFreeSpaces();

        for (int i = 0; i < ItemCount; i++) 
        {
            int itemId = builder.rand.Next(PossibleItems.Count);
            IItem item = PossibleItems[itemId];
            int tile = builder.rand.Next(0, possibleTiles.Count);
            if (possibleTiles.Count == 0) return;
            builder.addItem(item, possibleTiles[tile]);
            features |= Features.PickingUp | Features.Equipping;
        }

        for (int i = 0; i < WeaponCount; i++) 
        {
            int WeaponId = builder.rand.Next(PossibleWeapons.Count);
            IItem weapon = PossibleWeapons[WeaponId];
            int tile = builder.rand.Next(possibleTiles.Count);
            if (possibleTiles.Count == 0) return;
            builder.addItem(weapon, possibleTiles[tile]);
            features |= Features.PickingUp | Features.Equipping | Features.Attacking;
        }
    }

}
