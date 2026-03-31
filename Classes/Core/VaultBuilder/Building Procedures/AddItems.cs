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
using Vault_Scavanger.Interfaces.ItemInterfaces;

namespace Vault_Scavanger.Classes.Core.VaultBuilder.Building_Procedures;

public class AddItems : IBuildProcedure
{
    int ItemCount;
    int WeaponCount;
    List<IItem> PossibleItems;
    List<Weapon> PossibleWeapons;

    public AddItems(int ItemCount, int WeaponCount = 0)
    {
        this.ItemCount = ItemCount;
        PossibleItems = new List<IItem>()
        {
            new MiscellaneousItem { Name = "Empty Sunset Sarsaparilla bottle", Symbol = 'E', Value = 2 },
            new MiscellaneousItem { Name = "Big Empty Sunset Sarsaparilla bottle", Symbol = 'B', Value = 2 },
            new MiscellaneousItem { Name = "Bobby Pin", Symbol = 'B', Value = 0 },

        };

        this.WeaponCount = WeaponCount;
        PossibleWeapons = new List<Weapon>()
        {
            new RangedWeapon ("BB Gun", 'g', 36, true, 4 ),
            new RangedWeapon("10mm Pistol", '¬', 250, false, 22),
            new MeleeWeapon("Rolling Pin", 'R', 10, false, 3),
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
