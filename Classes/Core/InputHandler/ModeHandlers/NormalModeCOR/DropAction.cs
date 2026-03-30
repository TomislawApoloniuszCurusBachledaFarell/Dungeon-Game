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

public class DropAction : NormalHandler
{
    public DropAction() : base() { this.SetNext(new EquipAction()); }
    protected override bool canHandle(ConsoleKey PressedKey, KeyDefinitions keyBinds)
    {
        return PressedKey == keyBinds.GetActionKey(GameActions.Drop);
    }

    protected override InputIResult Process(ConsoleKey PressedKey, Player player, Maze maze, KeyDefinitions KeyBinds, ref InputMode inputMode, ref int? tempItemIndex)
    {
        InputIResult result = new InputIResult() { success = true };
        if (player.isDropPossible() != DropOptions.None)
        {
            inputMode = InputMode.Drop;
            result.resultMessage = InputMessages.DropSuccess();
        }
        else
        {
            result.resultMessage = InputMessages.DropFailure();
        }
        return result;
    }
}
