using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Classes.Core;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Interfaces.ItemInterfaces;
using Vault_Scavanger.Classes.Core.InputHandler.ModeHandlers;

namespace Maze_Mania.Classes.Items.Miscellaneous;

public class MiscellaneousItem : IInventoryItem
{
    public string Name { get; set; }
    public char Symbol { get; set; }
    public bool CanPickUpWhenInventoryFull => false;
    public int Value { get; set; }
    public bool TwoHanded { get; set; } = false;
    public void PickUp(Inventory inv) => inv.AddItem(this);
    public InputIResult TrySelecting(Player player) => new InputIResult {success = false, resultMessage = InputMessages.ItemHasNoUse() };
}
