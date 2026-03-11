using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Classes.Items.Currency;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Interfaces;

namespace Maze_Mania.Classes.Core;

public class Inventory
{
    public List<IEquiplable> items;
    public Currency currency;
    public Hands hands;
    public int MaxCapacity = 10;
    public Inventory()
    {
        items = new List<IEquiplable>();
        currency = new Currency();
        this.hands = new Hands();
    }
    public bool AddItem(IEquiplable item) {
        if (items.Capacity == MaxCapacity) return false;
        items.Add(item);
        return true;
    }
    public IEquiplable? RemoveItem(int i)
    {
        IEquiplable? item = null;
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

    public List<bool> getAllItemHandability()
    {
        List<bool>bools = new List<bool>();
        foreach (var item in items)
            bools.Add(item.TwoHanded);
        return bools;
    }

    public bool CanEquipTwoHanded(int index)
    {
        return HasFreeSpace() || !hands.isOccupied[0] || !hands.isOccupied[1] || isTwoHandedEquipped();
    }
    public bool isTwoHandedEquipped() {
        if(hands.itemSlot[0] == null) return false;
        return hands.itemSlot[0].TwoHanded;
    }

    public bool PlaceInHand(int index, char key)
    {
        bool didSucceed = false;

        switch (key)
        {
            case 'l':
                didSucceed = PlaceInCertainHand(index, 0); 
                break;
            case 'r':
                didSucceed = PlaceInCertainHand(index, 1);
                break;
            default:
                didSucceed = PlaceInBothHands(index);
                break;
        }
        return didSucceed;
    }
    public bool Unequip(char key)
    {
        switch (key)
        {
            case 'l':
                return GetItemFromHand(0);
            case 'r':
                return GetItemFromHand(1);
            default:
                return false;
        }
    }

    private bool PlaceInBothHands(int itemId)
    {
        bool result = true;
        if (itemId + 1 > items.Count) return false;
        IEquiplable? item = hands.itemSlot[1];

        result = ForceInCertainHand(items[itemId], 1);
        result = result && PlaceInCertainHand(itemId, 0);
        if (item != null && !item.TwoHanded)
            AddItem(item);
        
        return result;
    }

    private bool PlaceInCertainHand(int itemId, int handId)
    {
        if (itemId + 1 > items.Count || itemId < 0) return false;
        IEquiplable item = hands.itemSlot[handId];
        hands.itemSlot[handId] = items[itemId];
        if (item != null){

            if (item.TwoHanded)
            {
                if (items[itemId].TwoHanded)
                {
                    hands.itemSlot[1 - handId] = items[itemId];
                }
                else
                {

                    hands.itemSlot[1 - handId] = null;
                    hands.isOccupied[1 - handId] = false;
                }
            }

            items[itemId] = item;
        }
        else
            RemoveItem(itemId);
        hands.isOccupied[handId] = true;
        return true;
    }

    private bool ForceInCertainHand(IEquiplable item, int handId)
    {
        if (items == null) return false;
        hands.itemSlot[handId] = item;
        
        hands.isOccupied[handId] = true;
        return true;
    }

    private bool GetItemFromHand(int handId)
    {

        if (!hands.isOccupied[handId] || !HasFreeSpace()) return false;

        IEquiplable? item = hands.itemSlot[(handId)];
        if(item == null || !AddItem(item)) return false;
        if (isTwoHandedEquipped())
        {
            hands.itemSlot[1 - handId] = null;
            hands.isOccupied[1 - handId] = false;
        }
        hands.itemSlot[handId] = null;
        hands.isOccupied[handId] = false;
        return true;
    } 
    public bool HasFreeSpace() => items.Count < MaxCapacity;

    public class Currency
    {
        public int BottleCaps { get; set; }
        public int GoldBars { get; set; }
    }
}

