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

public abstract class Drug : IInventoryItem
{
    public string Name { get; set; }
    public char Symbol { get; set; }
    public bool CanPickUpWhenInventoryFull => false;
    public int Value { get; set; }
    public void PickUp(Inventory inv) => inv.AddItem(this);
    public bool canBeSelected(Inventory inv) => true;
    public List<Effect> effects;
    public abstract InputIResult TrySelecting(Player player, ref InputMode inputMode, int temp); 
    public InputIResult TryEquipping(Player player, int num, BodyParts Bodypart) => new InputIResult { success = false, resultMessage = InputMessages.UnexpectedBehaviour() };

}
