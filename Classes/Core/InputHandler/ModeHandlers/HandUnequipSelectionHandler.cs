using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Enums;
using Maze_Mania.Interfaces.CoreInterfaces;

namespace Maze_Mania.Classes.Core.InputHandlers.ModeHandlers;

public class HandUnequipSelectionHandler : IModeHandler
{
    public bool HandleKey(char key, Player player, Maze maze, ref InputMode inputMode, ref int? tempItemIndex)
    {
        switch (key)
        {
            case 'l':
            case 'r':
                if (player.Unequip(key))
                    inputMode = InputMode.Normal;

                break;
            case 'n':
                inputMode = InputMode.Normal;
                break;
            default:
                break;
        }
        return true;
    }

}
