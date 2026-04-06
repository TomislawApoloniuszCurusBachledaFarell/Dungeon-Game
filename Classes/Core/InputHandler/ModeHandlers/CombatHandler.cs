using Maze_Mania.Classes.Core;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Enums;
using Maze_Mania.Interfaces.CoreInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Core.AttackType;
using Vault_Scavanger.Classes.Items.Equipable;
using Vault_Scavanger.Classes.Utilis;
using Vault_Scavanger.Enums;
using Vault_Scavanger.Interfaces.CoreInterfaces;
using Vault_Scavanger.Interfaces.ItemInterfaces;

namespace Vault_Scavanger.Classes.Core.InputHandler.ModeHandlers;

public class CombatHandler : IModeHandler
{
    public InputIResult HandleKey(ConsoleKey key, Player player, Maze maze, KeyDefinitions KeyBinds, ref InputMode inputMode, ref int? handIndex)
    {
        Enemy enemy = maze.GetEnemyFrom(player.yPos, player.xPos);
        InputIResult result = new InputIResult();
        result.success = true;
        if (enemy == null) 
        {
            result.resultMessage = InputMessages.UnexpectedBehaviour();
            return result;
        }
        IAttackType attackType;
        if (key > ConsoleKey.D0 && key < ConsoleKey.D9)
        {
            int index = key - ConsoleKey.D0;
            if (index < AttackTypeManager.AttackTypes.Count)
            {
                attackType = AttackTypeManager.GetAttack(index);
            }
            else
            {
                result.resultMessage = InputMessages.NoFunction();
                return result;
            }
        }
        else if (key == KeyBinds.GetActionKey(GameActions.CancelAction))
        {
            if(player.CanSelectAttackHand())
                inputMode = InputMode.AttackHandSelection;
            else
                inputMode = InputMode.Normal;
            result.resultMessage = InputMessages.ActionCancelled(inputMode);
            return result;
        }
        else
        {
            result.resultMessage = InputMessages.NoFunction();
            return result;
        }

        result = ResolveCombat(player, enemy, attackType, handIndex);
        if (enemy.Stats.GetStatValue(StatType.health) < 0)
        {
            result.resultMessage = $"{result.resultMessage}. {InputMessages.EnemyDefeated(enemy.Name)}";
            maze.KillEnemy(enemy);
            inputMode = InputMode.Normal;
        }
        else
        {
            if(player.CanSelectAttackHand())
                inputMode= InputMode.AttackHandSelection;
        }
        return result;
    }

    private InputIResult ResolveCombat(Player player, Enemy enemy, IAttackType attackType, int? handIndex)
    {
        IEquipable? item;
        if (player.CanSelectAttackHand())
        {
            item = player.inventory.ItemInHand((BodyParts)handIndex);
        }
        else
        {
            item = player.inventory.ItemInHand(BodyParts.LeftHand);
        }
        BaseEquipable attackItem = new BaseEquipable() { Name = "bare fist" };
        if(item != null)
        {
            attackItem = (BaseEquipable)item;
        }
        string itemName = attackItem.Name;
        int dmg = attackItem.Accept(attackType, player, enemy);
        enemy.Stats.ModifyStat(StatType.health, (-1) * dmg);
        InputIResult result = new InputIResult();
        result.resultMessage = InputMessages.DealtDamage(itemName, dmg, enemy.Name);

        result.success = true;
        return result;

    } 
    
}
