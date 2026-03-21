using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Enums;
using Maze_Mania.Interfaces;

namespace Maze_Mania.Classes.Core;

public class Player
{
    public int xPos { get; private set; }
    public int yPos { get; private set; }
    public List<Stats> stats { get; private set; }
    public Inventory inventory;
    public Player(int xPos, int yPos)
    {
        this.xPos = xPos;
        this.yPos = yPos;
        stats = new List<Stats>();
        SetUpStats();
        inventory = new Inventory();
    }

    public void setPos(int xPos, int yPos)
    {
        this.xPos = xPos;
        this.yPos = yPos;
    }

    public void pickUpItem(IItem item)
    {
        if (item != null) item.PickUp(inventory);
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
    public List<bool> getAllItemHandability() => inventory.getAllItemHandability();


    public bool Equip(int? index, char key)
    {
        if(index == null) return false;
        return inventory.PlaceInHand(index.Value, key);
    }
    public bool IsTwoHandedEquipped() => inventory.isTwoHandedEquipped();
    public bool Unequip(char key) => inventory.Unequip(key);
    public bool CanEquipTwoHanded(int index) => inventory.CanEquipTwoHanded(index);
    public bool isRightHandOccupied() => inventory.hands.isOccupied[1];
    public bool isLeftHandOccupied() => inventory.hands.isOccupied[0];
    public bool isTwoHanded(int index) => index >= 0 && index < inventory.items.Count && inventory.items[index].TwoHanded;
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
        return inventory.items.Count > 0;
    }

    public void SetUpStats(int health = 100, int strength = 10, int agility = 10, int luck = 10, int inteligence = 10, int perception = 10)
    {
        stats.Add(new Stats("Health", health, 1));
        stats.Add(new Stats("Strength", strength, 2));

        stats.Add(new Stats("Perception", perception, 2));
        stats.Add(new Stats("Inteligence", inteligence, 2));

        stats.Add(new Stats("Agility", agility, 2));
        stats.Add(new Stats("Luck", luck, 2));
    }
};