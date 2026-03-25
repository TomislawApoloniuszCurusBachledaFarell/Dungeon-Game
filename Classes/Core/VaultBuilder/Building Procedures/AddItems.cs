using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Classes.Items.Currency;
using Maze_Mania.Classes.Items.Miscellaneous;
using Maze_Mania.Interfaces.ItemInterfaces;
using Vault_Scavanger.Interfaces.CoreInterfaces;

namespace Vault_Scavanger.Classes.Core.VaultBuilder.Building_Procedures;

public class AddItems : IBuildProcedure
{
    int Count;
    List<IItem> PossibleItems;

    public AddItems(int count)
    {
        Count = count;
        PossibleItems = new List<IItem>()
        {
            new MiscellaneousItem { Name = "Empty Sunset Sarsaparilla bottle", Symbol = 'E', Value = 2 },
            new MiscellaneousItem { Name = "Big Empty Sunset Sarsaparilla bottle", Symbol = 'B', Value = 2, TwoHanded = true },
            new MiscellaneousItem { Name = "Bobby Pin", Symbol = 'B', Value = 0, TwoHanded = false },
            new BottleCap(),
            new GoldBar(),
        };
    }

    public void Execute(VaultBuilder builder)
    {
        for (int i = 0; i < Count; i++) 
        {
            int itemId = builder.rand.Next(PossibleItems.Count);
            IItem item = PossibleItems[itemId];
            List<(int Y, int X)> possibleTiles = builder.GetFreeSpaces();
            int tile = builder.rand.Next(0, possibleTiles.Count);
            if (possibleTiles.Count == 0) return;
            builder.addItem(item, possibleTiles[tile]);
        }
    }

}
