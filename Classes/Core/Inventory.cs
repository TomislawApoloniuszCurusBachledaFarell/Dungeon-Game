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

    public InputIResult PlaceInHand(int index, char key)
    {
        InputIResult result = new InputIResult();
        result.success = false;
        string name = items[index].Name;
        string hand;
        switch (key)
        {
            case 'l':
                hand = "left hand";
                result.success = PlaceInCertainHand(index, 0); 
                break;
            case 'r':
                hand = "right hand";
                result.success = PlaceInCertainHand(index, 1);
                break;
            default:
                hand = "both hands";
                result.success = PlaceInBothHands(index);
                break;
        }
        result.resultMessage = $"{name} was placed in {hand}";
        if (!result.success) 
        {
            result.resultMessage = $"Couldn't place {name} in {hand}";
        }
        return result;
    }
    public InputIResult Unequip(char key)
    {
        InputIResult result = new InputIResult();
        switch (key)
        {
            case 'l':
                result.resultMessage = "Unequiped item from left hand";
                result.success = GetItemFromHand(0);
                if (!result.success)
                {
                    if (!hands.isOccupied[1])
                    {
                        result.resultMessage = "There is no items to be unequiped from left hand";
                    }
                    else
                    {
                        result.resultMessage = "There is no space in inventory to unequip the item to";
                    }
                }
                return result;

            case 'r':
                result.resultMessage = "Unequiped item from right hand";
                result.success = GetItemFromHand(1);
                if (!result.success)
                {
                    if (!hands.isOccupied[1])
                    {
                        result.resultMessage = "There is no items to be unequiped from left hand";
                    }
                    else
                    {
                        result.resultMessage = "There is no space in inventory to unequip the item to";
                    }
                }

                return result;

            default:
                result.resultMessage = "This key has no function here";
                result.success = false;
                return result;

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

