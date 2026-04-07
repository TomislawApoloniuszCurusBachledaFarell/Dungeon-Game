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
using Vault_Scavanger.Interfaces.CoreInterfaces;

namespace Vault_Scavanger.Classes.Items.Equipable.EquipableDecorator;

public class LifegiverDecorator : EquipableDecorator
{

    public LifegiverDecorator(BaseEquipable baseEquipable) : base(baseEquipable)
    {
    }
    public override string Name => $"Lifegiver {base.Name}";
    public override int Value => (int)(base.Value * (1.25));
    public override Effect effect => new Effect() { Type = Enums.StatType.health, Value = 25 };
    public override InputIResult TryEquipping(Player player, int InventoryIndex, BodyParts bodyPart)
    {
        InputIResult result = base.TryEquipping(player, InventoryIndex, bodyPart);
        if (result.success)
        {
            player.Stats.AddMaxStatEffect(effect);
        }
        return result;
    }
    public override InputIResult Unequip(Player player, BodyParts bodyPart)
    {
        InputIResult result = base.Unequip(player, bodyPart);
        if (result.success)
        {
            player.Stats.CancelMaxStatEffect(effect);
        }
        return result;
    }

}
