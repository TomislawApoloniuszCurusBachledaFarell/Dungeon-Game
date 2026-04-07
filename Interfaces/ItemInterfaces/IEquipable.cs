using Maze_Mania.Classes.Core;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Interfaces.ItemInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Enums;
using Vault_Scavanger.Interfaces.CoreInterfaces;

namespace Vault_Scavanger.Interfaces.ItemInterfaces;

public interface IEquipable : IInventoryItem
{
    public bool TwoHanded {  get; set; }

    public InputIResult Unequip(Player player, BodyParts bodyPart);
    public string Accept(IAttackType attack, ITarget attacker, ITarget defender);
}
