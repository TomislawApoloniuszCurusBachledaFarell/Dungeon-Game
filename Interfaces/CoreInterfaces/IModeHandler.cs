using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Classes.Core;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Enums;
using Vault_Scavanger.Classes.Core;

namespace Maze_Mania.Interfaces.CoreInterfaces;

public interface IModeHandler
{
    InputIResult HandleKey(ConsoleKey key, Player player, Maze maze, KeyDefinitions keyBinds, ref InputMode inputMode, ref int? tempItemIndex);
}
