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

public class ExitAction : NormalHandler
{
    public ExitAction() : base() { this.SetNext(new PickUpAction()); }
    protected override bool canHandle(ConsoleKey PressedKey, KeyDefinitions keyBinds)
    {
        return PressedKey == keyBinds.GetActionKey(GameActions.GameExit);
    }

    protected override InputIResult Process(ConsoleKey ckey, Player player, Maze maze, KeyDefinitions KeyBinds, ref InputMode inputMode, ref int? tempItemIndex)
    {
        return new InputIResult { resultMessage = InputMessages.ExitGame(), success = false };
    }
}
