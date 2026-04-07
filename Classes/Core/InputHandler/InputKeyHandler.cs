using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Classes.Core.InputHandlers;
using Maze_Mania.Enums;
using Maze_Mania.Interfaces.CoreInterfaces;
using Maze_Mania.Classes.Core.InputHandlers.ModeHandlers;
using Maze_Mania.Classes.Utilis;
using Vault_Scavanger.Classes.Core;
using Vault_Scavanger.Classes.Core.InputHandler.ModeActions.NormalModeCOR;
using Vault_Scavanger.Classes.Core.InputHandler.ModeHandlers;
using Vault_Scavanger.Classes.Core.InputHandler;

namespace Maze_Mania.Classes.Core.KeyHandlers;

public class InputHandler
{
    public Dictionary<InputMode, IModeHandler> ModeHandlers = new()
    {

        [InputMode.Normal] = new MovementAction(),
        [InputMode.Combat] = new CombatHandler(),
        [InputMode.PlayerDeath] = new PlayerDeathHandler(),
        [InputMode.AttackHandSelection] = new AttackHandSelectionHandler(),
        [InputMode.Drop] = new DropSelectionHandler(),
        [InputMode.Equip] = new EquipSelectionHandler(),
        [InputMode.HandSelection] = new HandSelectionHandler(),
        [InputMode.HandUnequipSelection] = new HandUnequipSelectionHandler()
    };


    public InputIResult HandleInput(ConsoleKey key, Player player, Maze maze,
                KeyDefinitions KeyBinds,  ref InputMode inputMode, ref int? tempItemIndex)
    {

        IModeHandler Handler;
        ModeHandlers.TryGetValue(inputMode, out Handler);
        return Handler.HandleKey(key, player, maze, KeyBinds, ref inputMode, ref tempItemIndex);
    }
}