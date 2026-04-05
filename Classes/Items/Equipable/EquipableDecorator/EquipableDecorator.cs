using Maze_Mania.Classes.Core;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Utilis;
using Vault_Scavanger.Enums;

namespace Vault_Scavanger.Classes.Items.Equipable.EquipableDecorator;

public class EquipableDecorator : BaseEquipable
{
    protected BaseEquipable baseEquipable;

    public EquipableDecorator(BaseEquipable baseEquipable) :
        base(baseEquipable.Name, baseEquipable.Symbol, baseEquipable.Value, baseEquipable.TwoHanded)
    {
        this.baseEquipable = baseEquipable;
    }

    public override string Name => baseEquipable.Name;
    public override int Value => baseEquipable.Value;
    public virtual Effect effect { get; }
    public override InputIResult TryEquipping(Player player, int InventoryIndex, BodyParts bodyPart)
    {
        return base.TryEquipping(player, InventoryIndex, bodyPart);
    }
    public override InputIResult Unequip(Player player, BodyParts bodyPart)
    {
        return base.Unequip(player, bodyPart);
    }
}
