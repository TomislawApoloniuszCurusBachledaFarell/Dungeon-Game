using Maze_Mania.Classes.Items.Other;
using Maze_Mania.Interfaces.ItemInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Items.Drug;
using Vault_Scavanger.Classes.Items.Equipable;
using Vault_Scavanger.Enums;

namespace Vault_Scavanger.Classes.Utilis.Factories;

public static class InventoryItemFactory
{
    public static IInventoryItem CreateRandomInventoryItem(Random rand)
    {
        int variant = rand.Next(5);
        IInventoryItem item = variant switch
        {
            0 => new MiscellaneousItem { Name = "Empty Sunset Sarsaparilla bottle", Symbol = 'E', Value = 2 },
            1 => new MiscellaneousItem { Name = "Big Empty Sunset Sarsaparilla bottle", Symbol = 'B', Value = 2 },
            2 => new MiscellaneousItem { Name = "Bobby Pin", Symbol = 'B', Value = 0 },
            3 => new TemporaryDrug
                {
                    Name = "Beer",
                    Symbol = 'b',
                    Value = 2,
                    Category = "alcohol",
                    effects = new List<Effect> { Effect.GetPositiveEffect(StatType.strength), Effect.GetNegativeEffect(StatType.inteligence) }
                },
            _ => new OneUseDrug
                {
                    Name = "Stimpak",
                    Symbol = 's',
                    Value = 75,
                    effects = new List<Effect> { Effect.GetPositiveEffect(StatType.health, 36) }
                }
        };

        return item;
    }
}
