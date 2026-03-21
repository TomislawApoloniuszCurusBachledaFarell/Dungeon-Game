using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Enums;
using Maze_Mania.Interfaces;

namespace Maze_Mania.Classes.Core.InputHandlers;

public class EquipHandler :IModeHandler
{
    public bool HandleKey(char key, Player player, Maze maze, ref InputMode inputMode, ref int? tempItemIndex)
    {
        if (char.IsDigit(key))
        {
            int num = key - '0';
            if (player.isTwoHanded(num))
            {
                if (player.CanEquipTwoHanded(num) && player.Equip(num, '?'))
                    inputMode = InputMode.Normal;
            }
            else
            {
                if (num < player.getAllItemNames().Count)
                {
                    tempItemIndex = num;
                    inputMode = InputMode.HandSelection;
                }
            }
        }

        return true;
    }


}
