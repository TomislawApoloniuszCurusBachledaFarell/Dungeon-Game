using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Classes.Core;
using Maze_Mania.Interfaces.ItemInterfaces;

namespace Maze_Mania.Classes.Items.Currency;

public class BottleCap : ICurrency
{
    public string Name => "Bottle Cap";
    public char Symbol => 'C';
    public void PickUp(Inventory inv) => AddCurrency(inv);
    public void AddCurrency(Inventory inv) => inv.currency.BottleCaps++;
}
