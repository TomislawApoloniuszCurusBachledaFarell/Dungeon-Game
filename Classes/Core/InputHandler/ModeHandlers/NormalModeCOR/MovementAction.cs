using Maze_Mania.Classes.Core;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Core.InputHandler.ModeHandlers;
using Vault_Scavanger.Classes.Utilis;
using Vault_Scavanger.Enums;

namespace Vault_Scavanger.Classes.Core.InputHandler.ModeActions.NormalModeCOR;

public class MovementAction : NormalHandler
{
    public MovementAction() : base() { this.SetNext(new DropAction()); }

    protected override bool canHandle(ConsoleKey PressedKey, KeyDefinitions keyBinds)
    {
        return PressedKey == keyBinds.GetActionKey(GameActions.MoveUp)
             || PressedKey == keyBinds.GetActionKey(GameActions.MoveDown)
             || PressedKey == keyBinds.GetActionKey(GameActions.MoveLeft)
             || PressedKey == keyBinds.GetActionKey(GameActions.MoveRight);
    }

    protected override InputIResult Process(ConsoleKey PressedKey, Player player, Maze maze, KeyDefinitions KeyBinds, ref InputMode inputMode, ref int? tempItemIndex)
    {
        InputIResult result;
        if (PressedKey == KeyBinds.GetActionKey(GameActions.MoveUp))
        {
            result = maze.MoveUp();
        }
        else if (PressedKey == KeyBinds.GetActionKey(GameActions.MoveDown))
        {
            result = maze.MoveDown();
        }
        else if (PressedKey == KeyBinds.GetActionKey(GameActions.MoveLeft))
        {
            result = maze.MoveLeft();
        }
        else
        {
            result = maze.MoveRight();
        }

        foreach (Enemy enemy in maze.Enemies)
        {
            if (enemy.yPos == player.yPos && enemy.xPos == player.xPos)
            {
                if (player.CanSelectAttackHand())
                {
                    inputMode = InputMode.AttackHandSelection;
                    result.resultMessage = InputMessages.EnteredHandSelection();
                }
                else
                {
                    inputMode = InputMode.Combat;
                    result.resultMessage = InputMessages.CombatStarted(result.resultMessage, enemy.Name);
                }
                return result;
            }
        }
        
        return result;
    }
}
