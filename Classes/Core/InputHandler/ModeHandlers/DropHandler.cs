using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Classes.Items.Currency;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Enums;
using Maze_Mania.Interfaces.CoreInterfaces;
using Vault_Scavanger.Classes.Core;
using Vault_Scavanger.Enums;

namespace Maze_Mania.Classes.Core.InputHandlers.ModeHandlers;

public class DropHandler : IModeHandler
{
    public InputIResult HandleKey(ConsoleKey key, Player player, Maze maze, KeyDefinitions KeyBinds, ref InputMode inputMode, ref int? tempItemIndex)
    {
        InputIResult result = new InputIResult();
        if (ConsoleKey.D0 <= key && key <= ConsoleKey.D9)
        {
            int num = key - ConsoleKey.D0;
            result = maze.PlayerDrops('-', num);
            if (result.success)
            {
                inputMode = InputMode.Normal;
            }
        }
        else
        {
            if (key == KeyBinds.GetActionKey(GameActions.CancelAction))
            {
                result.success = true;
                result.resultMessage = "Cancelled dropping items";
                inputMode = InputMode.Normal;
            }
            else if ((char)key == BottleCap.getChar || (char)key == GoldBar.getChar)
            {
                result = maze.PlayerDrops(Char.ToLower((char)key), -1);
                if (result.success)
                    inputMode = InputMode.Normal;
            }
            else
            {
                result.resultMessage = "This key has no function here";
            }
        }
        result.success = true;
        return result;
    }

}
