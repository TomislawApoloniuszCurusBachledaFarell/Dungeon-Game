using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Enums;
using Maze_Mania.Interfaces.ItemInterfaces;
using Vault_Scavanger.Classes.Core;
using Vault_Scavanger.Classes.Utilis;

namespace Maze_Mania.Classes.Core;

public class Player
{
    public int xPos { get; private set; }
    public int yPos { get; private set; }
    public StatManager stats { get; private set; }
    public Inventory inventory;
    public Player((int yPos, int xPos) Position)
    {
        this.xPos = Position.xPos;
        this.yPos = Position.yPos;
        stats = new StatManager();

        inventory = new Inventory();
    }

    public void setPos(int xPos, int yPos)
    {
        this.xPos = xPos;
        this.yPos = yPos;
    }
    public void UpdatePlayer() => stats.UpdateStats();
    public InputIResult pickUpItem(IItem item)
    {
        InputIResult result = new InputIResult();
        if (item != null)
        {
            item.PickUp(inventory);
            result.success = true;
            result.resultMessage = InputMessages.PickedUpAnItem(item.Name);
            return result;
        }
        result.success= true;
        result.resultMessage = InputMessages.UnexpectedBehaviour();
        return result ;
    }
   
    public IItem? dropItem(int index)
    {
        return inventory.RemoveItem(index);
    }

    public IItem? dropCurrency(char currency)
    {
        return inventory.RemoveCurrency(currency);
    }

    public List<string> getAllItemNames() => inventory.getAllItemNames();
    public List<bool> getAllItemsSelectability() => inventory.getAllItemsSelectability();
    public bool IsTwoHandedEquipped() => inventory.isTwoHandedEquipped();
    public bool isRightHandOccupied() => inventory.hands.isOccupied[1];
    public bool isLeftHandOccupied() => inventory.hands.isOccupied[0];
    public DropOptions isDropPossible()
    {
        DropOptions dropOptions = new DropOptions();
        if (inventory.items.Count > 0)
            dropOptions = DropOptions.Item;
        if (inventory.areBottleCaps())
            dropOptions = dropOptions | DropOptions.BottleCap;
        if (inventory.areGoldBars())
            dropOptions = dropOptions | DropOptions.GoldBar;
        return dropOptions;
    }
    public bool hasInventorySpace() => inventory.HasFreeSpace();
    public bool HasEquipable()
    {
        foreach (var item in inventory.items)
        {
            if(item.canBeSelected(inventory))
                return true;
        }
        return false;
    }

    
};