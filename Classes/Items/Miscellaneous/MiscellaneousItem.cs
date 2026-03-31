using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Classes.Core;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Enums;
using Maze_Mania.Interfaces.ItemInterfaces;
using Vault_Scavanger.Classes.Core.InputHandler.ModeHandlers;
using Vault_Scavanger.Enums;

namespace Maze_Mania.Classes.Items.Miscellaneous;

public class MiscellaneousItem : IInventoryItem
{
    public string Name { get; set; }
    public char Symbol { get; set; }
    public bool CanPickUpWhenInventoryFull => false;
    public int Value { get; set; }
    public void PickUp(Inventory inv) => inv.AddItem(this);
    public bool canBeSelected(Inventory inv) => false;
    public InputIResult TrySelecting(Player player, ref InputMode inputIMode, int num) => new InputIResult {success = false, resultMessage = InputMessages.ItemHasNoUse() };
    public InputIResult TryEquipping(Inventory inv, int num, BodyParts Bodypart) => new InputIResult {success = false, resultMessage = InputMessages.UnexpectedBehaviour() };
}
