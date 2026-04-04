using Maze_Mania.Classes.Items.Currency;
using Maze_Mania.Classes.Items.Other;
using Vault_Scavanger.Classes.Items.Equipable.Weapon;
using Maze_Mania.Interfaces.ItemInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Vault_Scavanger.Classes.Items.Drug;
using Vault_Scavanger.Classes.Utilis;
using Vault_Scavanger.Enums;
using Vault_Scavanger.Interfaces.CoreInterfaces;
using Vault_Scavanger.Interfaces.ItemInterfaces;
using Vault_Scavanger.Classes.Items.Equipable.Weapon.WeaponDecorators;

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
            new TemporaryDrug {Name = "Beer", Symbol = 'b', Value = 2, Category = "alcohol",
                effects = new List<Effect>{Effect.GetPositiveEffect(StatType.strength), Effect.GetNegativeEffect(StatType.inteligence)}},
            new OneUseDrug {Name = "Stimpak", Symbol = 's', Value = 75,
                effects = new List<Effect>{Effect.GetPositiveEffect(StatType.health, 36) } }
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
            Weapon weapon = PossibleWeapons[WeaponId];
            if(builder.rand.Next(5) == 0)
            {
                weapon = new RustyDecorator(weapon);
            }
            int tile = builder.rand.Next(possibleTiles.Count);
            if (possibleTiles.Count == 0) return;
            builder.addItem(weapon, possibleTiles[tile]);
            features |= Features.PickingUp | Features.Equipping | Features.Attacking;
        }
    }

}
