using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Enums;
using Maze_Mania.Interfaces.CoreInterfaces;

namespace Maze_Mania.Classes.Core.InputHandlers.ModeHandlers;

public class HandSelectionHandler : IModeHandler
{
    public bool HandleKey(char key, Player player, Maze maze, ref InputMode inputMode, ref int? tempItemIndex)
    {
        switch (key)
        {
            case 'l':
            case 'r':
                if (player.Equip(tempItemIndex, key))
                {
                    inputMode = InputMode.Normal;
                    tempItemIndex = null;
                }
                break;
            case 'n':
                inputMode = InputMode.Equip;
                break;
            default:
                break;
        }
        return true;
    }

}
