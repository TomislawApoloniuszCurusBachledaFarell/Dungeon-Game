using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Classes.Core;
using Maze_Mania.Interfaces.ItemInterfaces;

namespace Maze_Mania.Classes.Items.Currency;

public class GoldBar : ICurrency
{
    public string Name => getName;
    public char Symbol => getChar;
    public bool CanPickUpWhenInventoryFull => true;
    public static string getName => "Gold Bar";
    public static char getChar => 'G';
    public void PickUp(Inventory inv) => AddCurrency(inv);
    public void AddCurrency(Inventory inv) => inv.currency.GoldBars++;
}