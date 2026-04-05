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
using Vault_Scavanger.Classes.Items.Equipable.EquipableDecorator;
using Vault_Scavanger.Classes.Items.Equipable;
using Vault_Scavanger.Classes.Utilis.Factories;

namespace Vault_Scavanger.Classes.Core.VaultBuilder.Building_Procedures;

public class AddItems : IBuildProcedure
{
    int ItemCount;
    int WeaponCount;

    public AddItems(int ItemCount, int WeaponCount = 0)
    {
        this.ItemCount = ItemCount;
        this.WeaponCount = WeaponCount;
    }

    public void Execute(VaultBuilder builder, ref Features features)
    {
        List<(int Y, int X)> possibleTiles = builder.GetFreeSpaces();

        for (int i = 0; i < ItemCount; i++) 
        {
            IItem item = InventoryItemFactory.CreateRandomInventoryItem(builder.rand);
            int tile = builder.rand.Next(0, possibleTiles.Count);
            if (possibleTiles.Count == 0) return;
            builder.addItem(item, possibleTiles[tile]);
            features |= Features.PickingUp | Features.Equipping;
        }

        for (int i = 0; i < WeaponCount; i++) 
        {
            BaseEquipable weapon = WeaponFactory.CreateRandomWeapon(builder.rand);
            
            int tile = builder.rand.Next(possibleTiles.Count);
            if (possibleTiles.Count == 0) return;
            builder.addItem(weapon, possibleTiles[tile]);
            features |= Features.PickingUp | Features.Equipping | Features.Attacking;
        }
    }

}
