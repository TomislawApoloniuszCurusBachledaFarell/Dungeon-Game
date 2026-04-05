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

    public virtual InputIResult TrySelecting(Player player, ref InputMode inputMode, int InventoryIndex)
    {
        InputIResult result = new InputIResult();
        if (TwoHanded)
        {

            result = this.TryEquipping(player, InventoryIndex, BodyParts.BothHands);
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

    public virtual InputIResult TryEquipping(Player player ,int InventoryIndex, BodyParts bodyPart)
    {
        InputIResult result = new InputIResult();
        if (!canBeSelected(player.inventory))
        {
            result.success = false;
            result.resultMessage = InputMessages.FullInventory();
            return result;
        }
        player.inventory.RemoveItem(InventoryIndex);

        if((bodyPart & BodyParts.LeftHand) != 0)
        {
            IEquipable? item0 = player.inventory.hands.itemSlot[0];
            if (item0 != null)
                item0.Unequip(player, BodyParts.LeftHand);

            player.inventory.hands.itemSlot[0] = this;
            player.inventory.hands.isOccupied[0] = true;

        }

        if ((bodyPart & BodyParts.RightHand) != 0)
        {
            IEquipable? item1 = player.inventory.hands.itemSlot[1];
            if (item1 != null)
                item1.Unequip(player, BodyParts.LeftHand);

            player.inventory.hands.itemSlot[1] = this;
            player.inventory.hands.isOccupied[1] = true;
        }
        result.success = true;
        result.resultMessage = InputMessages.ItemWasPlacedIn(Name, bodyPart);
        return result;
    }
    public virtual InputIResult Unequip(Player player, BodyParts bodyPart)
    {
        InputIResult result = new InputIResult();
        bool wasUnequiped = false;
        if (TwoHanded)
        {
            bodyPart = BodyParts.BothHands;
        }
        if ((bodyPart & BodyParts.LeftHand) > 0)
        {
            player.inventory.hands.itemSlot[0] = null;
            player.inventory.hands.isOccupied[0] = false;
            if (!wasUnequiped)
            {
                player.inventory.items.Add(this);
                wasUnequiped = true;
            }
        }
        if((bodyPart & BodyParts.RightHand) > 0)
        {
            player.inventory.hands.itemSlot[1] = null;
            player.inventory.hands.isOccupied[1] = false;
            if (!wasUnequiped)
            {
                player.inventory.items.Add(this);
                wasUnequiped= true;
            }
        }
        result.resultMessage = InputMessages.UnequipedItem(bodyPart);
        result.success = true ;
        return result;
    }
    public void PickUp(Inventory inv) => inv.AddItem(this);
}
