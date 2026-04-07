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
using Vault_Scavanger.Classes.Utilis.Messages;
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
        if (key >= ConsoleKey.D0 && key <= ConsoleKey.D9)
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

        result = ResolveCombatPlayer(player, enemy, attackType, handIndex);
        if (!enemy.IsAlive())
        {
            result.resultMessage = $"{result.resultMessage}. {InputMessages.EnemyDefeated(enemy.Name)}";
            maze.KillEnemy(enemy);
            inputMode = InputMode.Normal;
            return result;
        }

        result.bonusMessage = ResolveCombatEnemy(player, enemy);
        if(!player.IsAlive())
        {
            result.bonusMessage = $"{result.bonusMessage}. You couldnt survive this attack";
            inputMode = InputMode.PlayerDeath;
            return result;
        }
        if (player.CanSelectAttackHand())
            inputMode = InputMode.AttackHandSelection;
        return result;
    }

    private InputIResult ResolveCombatPlayer(Player player, Enemy enemy, IAttackType attackType, int? handIndex)
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
        if (item == null)
        {
            item = new BaseEquipable() { Name = "bare fist" };
        }

        string itemName = item.Name;
        string combatResult = item.Accept(attackType, player, enemy);


        InputIResult result = new InputIResult();
        result.resultMessage = combatResult;
        
        result.success = true;
        return result;

    } 
    
    private string ResolveCombatEnemy(Player player, Enemy enemy)
    {
        BaseEquipable unarmed = new BaseEquipable();
        string combatResult = unarmed.Accept(AttackTypeManager.GetAttack(0), enemy, player);
;       
        return combatResult;
    }
}
