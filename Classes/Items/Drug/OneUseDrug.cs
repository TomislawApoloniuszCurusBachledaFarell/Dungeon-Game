using Maze_Mania.Classes.Core;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Vault_Scavanger.Classes.Utilis;

namespace Vault_Scavanger.Classes.Items.Drug;

public class OneUseDrug : Drug
{
    public override InputIResult TrySelecting(Player player, ref InputMode inputIMode, int num)
    {
        player.Stats.AddEffect(effects);
        player.inventory.RemoveItem(this);
        inputIMode = InputMode.Normal;
        return new InputIResult { success = true, resultMessage = InputMessages.DrugWasTaken(Name) };
    }
}
