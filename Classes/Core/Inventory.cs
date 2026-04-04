using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Classes.Items.Currency;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Interfaces.ItemInterfaces;
using Vault_Scavanger.Classes.Core.InputHandler.ModeHandlers;
using Vault_Scavanger.Enums;
using Vault_Scavanger.Interfaces.ItemInterfaces;

namespace Maze_Mania.Classes.Core;

public class Inventory
{
    public List<IInventoryItem> items;
    public Currency currency;
    public Hands hands;
    public int MaxCapacity = 10;
    public Inventory()
    {
        items = new List<IInventoryItem>();
        currency = new Currency();
        this.hands = new Hands();
    }
    public bool AddItem(IInventoryItem item) {
        if (items.Capacity == MaxCapacity) return false;
        items.Add(item);
        return true;
    }
    public void RemoveItem(IInventoryItem item)
    {
        items.Remove(item);
    }

    public IInventoryItem? RemoveItem(int i)
    {
        IInventoryItem? item = null;
        if (i <  items.Count && i >= 0)
        {
            item = items[i];
            items.RemoveAt(i);
        }
        return item;
    }
    public IItem? RemoveCurrency(char currency)
    {
        IItem? item = null;
        switch (currency)
        {
            case 'c':
                item = new BottleCap();
                this.currency.BottleCaps--;
                break;
            case 'g':
                item = new GoldBar();
                this.currency.GoldBars--;
                break;
            default:
                break;
        }
        return item;
    }

    public IEquipable? ItemInHand(BodyParts bodyParts)
    {
        switch (bodyParts)
        {
            case BodyParts.BothHands:
            case BodyParts.LeftHand:
                return hands.itemSlot[0];
            case BodyParts.RightHand:
                return hands.itemSlot[1];
        }
        return null;
    }

    public int getNumberOfItems() => items.Count;
    public bool areBottleCaps()
    {
        return currency.BottleCaps > 0;
    }

    public bool areGoldBars()
    {
        return currency.GoldBars > 0;
    }

    public List<string> getAllItemNames()
    {
        List<string> strings = new List<string>();
        foreach (var item in items) 
            strings.Add(item.Name);
        return strings;
    }

    public List<bool> getAllItemsSelectability()
    {
        List<bool>bools = new List<bool>();
        foreach (var item in items)
            bools.Add(item.canBeSelected(this));
        return bools;
    }

    public bool CanEquipTwoHanded()
    {
        return HasFreeSpace() || !hands.isOccupied[0] || !hands.isOccupied[1] || isTwoHandedEquipped();
    }
    public bool isTwoHandedEquipped() {
        if(hands.itemSlot[0] == null) return false;
        return hands.itemSlot[0].TwoHanded;
    }

    public bool HasFreeSpace() => items.Count < MaxCapacity;

    public class Currency
    {
        public int BottleCaps { get; set; }
        public int GoldBars { get; set; }
    }
}

