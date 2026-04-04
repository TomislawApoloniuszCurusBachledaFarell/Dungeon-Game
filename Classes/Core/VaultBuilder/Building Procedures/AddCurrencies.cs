using Maze_Mania.Classes.Items.Currency;
using Maze_Mania.Classes.Items.Other;
using Vault_Scavanger.Classes.Items.Equipable.Weapon;

using Maze_Mania.Interfaces.ItemInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Enums;
using Vault_Scavanger.Interfaces.CoreInterfaces;

namespace Vault_Scavanger.Classes.Core.VaultBuilder.Building_Procedures;


public class AddCurrencies : IBuildProcedure
{
    int Count;
    List<ICurrency> PossibleCurrencies;

    public AddCurrencies(int count)
    {
        Count = count;
        PossibleCurrencies = new List<ICurrency>()
        {
            new BottleCap(),
            new GoldBar(),
        };
    }

    public void Execute(VaultBuilder builder, ref Features features)
    {
        for (int i = 0; i < Count; i++)
        {
            int itemId = builder.rand.Next(PossibleCurrencies.Count);
            IItem item = PossibleCurrencies[itemId];
            List<(int Y, int X)> possibleTiles = builder.GetFreeSpaces();
            int tile = builder.rand.Next(possibleTiles.Count);
            if (possibleTiles.Count == 0) return;

            builder.addItem(item, possibleTiles[tile]);
            features |= Features.PickingUp;
        }
    }

}
