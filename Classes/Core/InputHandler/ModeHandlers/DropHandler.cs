using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Enums;
using Maze_Mania.Interfaces.CoreInterfaces;

namespace Maze_Mania.Classes.Core.InputHandlers.ModeHandlers;

public class DropHandler : IModeHandler
{
    public bool HandleKey(char key, Player player, Maze maze, ref InputMode inputMode, ref int? tempItemIndex)
    {
        if (char.IsDigit(key))
        {
            int num = key - '0';
            if (maze.PlayerDrops(key, num))
            {
                inputMode = InputMode.Normal;
            }
        }
        else
        {
            switch (key)
            {
                case 'n':
                    inputMode = InputMode.Normal;
                    break;
                case 'c':
                case 'g':
                    if (maze.PlayerDrops(key, -1))
                        inputMode = InputMode.Normal;
                    break;
                default:
                    break;
            }
        }
        return true;
    }

}
