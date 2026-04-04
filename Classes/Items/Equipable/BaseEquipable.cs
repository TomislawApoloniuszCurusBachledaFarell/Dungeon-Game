using Maze_Mania.Classes.Core;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Enums;
using Maze_Mania.Interfaces.ItemInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Utilis;
using Vault_Scavanger.Enums;
using Vault_Scavanger.Interfaces.ItemInterfaces;

namespace Vault_Scavanger.Classes.Items.Equipable;

public class BaseEquipable : IEquipable
{
    public virtual string Name { get; set; }
    public char Symbol { get; set; }
    public virtual int Value { get; set; }
    public bool CanPickUpWhenInventoryFull { get; } = false;
    public bool TwoHanded { get; set; }
 
    public BaseEquipable(string name, char symbol, int value, bool twoHanded)
    {
        Name = name;
        Symbol = symbol;
        Value = value;
        TwoHanded = twoHanded;
    }

    public bool canBeSelected(Inventory inv)
    {
        if (TwoHanded)
        {
            return inv.CanEquipTwoHanded();
        }
        return true;
    }

    public InputIResult TrySelecting(Player player, ref InputMode inputMode, int InventoryIndex)
    {
        InputIResult result = new InputIResult();
        if (TwoHanded)
        {

            result = this.TryEquipping(player.inventory, InventoryIndex, BodyParts.BothHands);
            if (result.success)
                inputMode = InputMode.Normal;
            result.success = false;

        }
        else
        {
            inputMode = InputMode.HandSelection;
            result.success = true;
        }
        return result;
    }

    public InputIResult TryEquipping(Inventory inv ,int InventoryIndex, BodyParts bodyPart)
    {
        InputIResult result = new InputIResult();
        if (!canBeSelected(inv))
        {
            result.success = false;
            result.resultMessage = InputMessages.FullInventory();
            return result;
        }
        inv.RemoveItem(InventoryIndex);

        if((bodyPart & BodyParts.LeftHand) != 0)
        {
            IInventoryItem? item0 = inv.hands.itemSlot[0];
            inv.hands.itemSlot[0] = this;
            inv.hands.isOccupied[0] = true;
            if (item0 != null)
                item0.PickUp(inv);
        }

        if ((bodyPart & BodyParts.RightHand) != 0)
        {
            IInventoryItem? item1 = inv.hands.itemSlot[1];
            inv.hands.itemSlot[1] = this;
            inv.hands.isOccupied[1] = true;
            if (item1 != null)
                item1.PickUp(inv);
        }
        result.success = true;
        result.resultMessage = InputMessages.ItemWasPlacedIn(Name, bodyPart);
        return result;
    }
    public InputIResult Unequip(Inventory inv, BodyParts bodyPart)
    {
        InputIResult result = new InputIResult();
        bool wasUnequiped = false;
        if ((bodyPart & BodyParts.LeftHand) > 0)
        {
            inv.hands.itemSlot[0] = null;
            inv.hands.isOccupied[0] = false;
            if (!wasUnequiped)
            {
                inv.items.Add(this);
                wasUnequiped = true;
            }
        }
        if((bodyPart & BodyParts.RightHand) > 0)
        {
            inv.hands.itemSlot[1] = null;
            inv.hands.isOccupied[1] = false;
            if (!wasUnequiped)
            {
                inv.items.Add(this);
                wasUnequiped= true;
            }
        }
        result.resultMessage = InputMessages.UnequipedItem(bodyPart);
        result.success = true ;
        return result;
    }
    public void PickUp(Inventory inv) => inv.AddItem(this);
}
