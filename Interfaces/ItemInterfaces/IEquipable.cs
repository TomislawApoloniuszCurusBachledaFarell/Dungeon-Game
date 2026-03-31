using Maze_Mania.Classes.Core;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Interfaces.ItemInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Enums;

namespace Vault_Scavanger.Interfaces.ItemInterfaces;

public interface IEquipable : IInventoryItem
{
    public bool TwoHanded {  get; set; }

    public InputIResult Unequip(Inventory inv, BodyParts bodyPart);
}
