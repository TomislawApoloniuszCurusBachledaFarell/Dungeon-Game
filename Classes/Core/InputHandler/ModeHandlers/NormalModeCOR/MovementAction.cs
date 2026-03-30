using Maze_Mania.Classes.Core;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Core.InputHandler.ModeHandlers;
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
        if (PressedKey == KeyBinds.GetActionKey(GameActions.MoveUp))
        {
            return maze.MoveUp();
        }
        else if (PressedKey == KeyBinds.GetActionKey(GameActions.MoveDown))
        {
            return maze.MoveDown();
        }
        else if (PressedKey == KeyBinds.GetActionKey(GameActions.MoveLeft))
        {
            return maze.MoveLeft();
        }
        else
        {
            return maze.MoveRight();
        }
    }
}
