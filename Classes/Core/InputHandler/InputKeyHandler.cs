using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Classes.Core.InputHandlers;
using Maze_Mania.Enums;
using Maze_Mania.Interfaces.CoreInterfaces;
using Maze_Mania.Classes.Core.InputHandlers.ModeHandlers;

namespace Maze_Mania.Classes.Core.KeyHandlers;

public class InputHandler
{
    public Dictionary<InputMode, IModeHandler> ModeHandlers = new()
    {
        [InputMode.Normal] = new NormalHandler(),
        [InputMode.Drop] = new DropHandler(),
        [InputMode.Equip] = new EquipHandler(),
        [InputMode.HandSelection] = new HandSelectionHandler(),
        [InputMode.HandUnequipSelection] = new HandUnequipSelectionHandler()
    };

    public bool HandleInput(char key, Player player, Maze maze,
                ref InputMode inputMode, ref int? tempItemIndex)
    {
        key = Char.ToLower(key);

        IModeHandler Handler;
        ModeHandlers.TryGetValue(inputMode, out Handler);
        return Handler.HandleKey(key, player, maze, ref inputMode, ref tempItemIndex);
    }
}