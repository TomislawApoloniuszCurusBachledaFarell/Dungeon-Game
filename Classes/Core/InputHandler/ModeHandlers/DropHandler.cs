using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Enums;
using Maze_Mania.Interfaces.CoreInterfaces;

namespace Maze_Mania.Classes.Core.InputHandlers.ModeHandlers;

public class DropHandler : IModeHandler
{
    public InputIResult HandleKey(char key, Player player, Maze maze, ref InputMode inputMode, ref int? tempItemIndex)
    {
        InputIResult result = new InputIResult();
        if (char.IsDigit(key))
        {
            int num = key - '0';
            result = maze.PlayerDrops(key, num);
            if (result.success)
            {
                inputMode = InputMode.Normal;
            }
        }
        else
        {
            switch (key)
            {
                case 'n':
                    result.success = true;
                    result.resultMessage = "Cancelled dropping items";
                    inputMode = InputMode.Normal;
                    break;
                case 'c':
                case 'g':
                    result = maze.PlayerDrops(key, -1);
                    if (result.success)
                        inputMode = InputMode.Normal;
                    break;
                default:
                    result.resultMessage = "This key has no function here";
                    break;
            }
        }
        result.success = true;
        return result;
    }

}
