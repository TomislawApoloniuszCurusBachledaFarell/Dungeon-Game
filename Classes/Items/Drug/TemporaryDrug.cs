using Maze_Mania.Classes.Core;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Enums;
using Maze_Mania.Interfaces.ItemInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Utilis;
using Vault_Scavanger.Enums;

namespace Vault_Scavanger.Classes.Items.Drug;

public class TemporaryDrug : Drug
{
    public string Category;
    public int EffectDuration = 10;
    public override InputIResult TrySelecting(Player player, ref InputMode inputIMode, int num) {
        player.stats.AddEffect(effects, Category, EffectDuration);
        player.inventory.RemoveItem(this);
        inputIMode = InputMode.Normal;
        return new InputIResult { success = true, resultMessage = InputMessages.DrugWasTaken(Name) };
     }
}
