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
using Vault_Scavanger.Classes.Utilis.Messages;
using Vault_Scavanger.Enums;
using Vault_Scavanger.Interfaces.CoreInterfaces;
using Vault_Scavanger.Interfaces.ItemInterfaces;

namespace Maze_Mania.Classes.Core;

public class Player : ITarget
{
    public int xPos { get; private set; }
    public int yPos { get; private set; }
    public StatManager Stats { get; set; }
    public Inventory inventory;
    public string Name { get => "Vault Explorer"; }
    public Player((int yPos, int xPos) Position)
    {
        this.xPos = Position.xPos;
        this.yPos = Position.yPos;
        Stats = new StatManager();
        setUpStats();
        inventory = new Inventory();
    }

    private void setUpStats()
    {
        Stats.ImplementStat(StatType.health, 100, 100);
        Stats.ImplementStat(StatType.strength);
        Stats.ImplementStat(StatType.perception);
        Stats.ImplementStat(StatType.inteligence);
        Stats.ImplementStat(StatType.agility);
        Stats.ImplementStat(StatType.luck);
        Stats.ImplementStat(StatType.baseDmg, 2, 2, false);
        Stats.ImplementStat(StatType.armour, 100, 2, false);
    }
    public void setPos(int xPos, int yPos)
    {
        this.xPos = xPos;
        this.yPos = yPos;
    }
    public void UpdatePlayer() => Stats.UpdateStats();
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

    public bool CanSelectAttackHand()
    {
        bool doesntNeeedTo = IsTwoHandedEquipped() || 
            (inventory.ItemInHand(BodyParts.LeftHand) == null && inventory.ItemInHand(BodyParts.RightHand) == null);
        return !doesntNeeedTo;  
    }

    public void TakeDamage(int dmg)
    {
        Stats.ModifyStat(StatType.health, (-1) * dmg);
    }

    public bool IsAlive() => Stats.GetStatValue(StatType.health) > 0;

    public List<string> GetVisibleStats()
    {
        List<string> statsString = new List<string>();
        foreach (Stats stat in Stats.Stats.Values)
        {
            string? statString = stat.ToString();
            if (statString != null)
                statsString.Add(statString);
        }
        return statsString;
    }
};