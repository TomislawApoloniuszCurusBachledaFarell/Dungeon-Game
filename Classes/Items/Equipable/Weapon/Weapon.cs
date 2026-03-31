using Maze_Mania.Classes.Core;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Enums;
using Maze_Mania.Interfaces.ItemInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze_Mania.Classes.Items.Weapon;

public abstract class Weapon : IWeapon
{
    public string Name { get; set; }
    public char Symbol { get; set; }
    public bool CanPickUpWhenInventoryFull => false;
    public int Value { get; set; }
    public int Damage { get; set; }
    public bool TwoHanded { get; set; }
    public void PickUp(Inventory inv) => inv.AddItem(this);
    protected Weapon(string name, char symbol, int value, int damage, bool twoHanded)
    {
        Name = name;
        Symbol = symbol;
        Value = value;
        Damage = damage;
        TwoHanded = twoHanded;
    }

    public InputIResult TrySelecting(Player player, InputMode inputMode, int InventoryIndex) 
    {
        InputIResult result = new InputIResult();
        if (TwoHanded)
        {
            if (player.CanEquipTwoHanded())
            {
                result = player.Equip(InventoryIndex, '?');
                if (result.success)
                    inputMode = InputMode.Normal;
            }
        }
        else
        {
            inputMode = InputMode.HandSelection;
            result.success = true;
        }
        return result;
    }
}
