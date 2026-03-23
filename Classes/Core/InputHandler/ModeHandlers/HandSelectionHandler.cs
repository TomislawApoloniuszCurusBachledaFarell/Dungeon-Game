using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Enums;
using Maze_Mania.Interfaces.CoreInterfaces;

namespace Maze_Mania.Classes.Core.InputHandlers.ModeHandlers;

public class HandSelectionHandler : IModeHandler
{
    public InputIResult HandleKey(char key, Player player, Maze maze, ref InputMode inputMode, ref int? tempItemIndex)
    {
        InputIResult result = new InputIResult();
        switch (key)
        {
            case 'l':
            case 'r':
                result = player.Equip(tempItemIndex, key);
                if (result.success)
                {
                    inputMode = InputMode.Normal;
                    tempItemIndex = null;
                }
                break;
            case 'n':
                result.resultMessage = "Cancelled selecting hands";
                inputMode = InputMode.Equip;
                break;
            default:
                result.resultMessage = "This key has no function here";
                break;
        }
        result.success = true;
        return result;
    }

}
