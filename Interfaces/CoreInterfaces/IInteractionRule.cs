using Maze_Mania.Classes.Core;
using Maze_Mania.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Core.InteractionFinder;

namespace Vault_Scavanger.Interfaces.CoreInterfaces;

public interface IInteractionRule
{
    public bool canInteract(Maze maze, Player player, InputMode inputMode);

    public List<string> GetInteractions(Maze maze, Player player, InputMode inputMode, InteractionMessages messages);
}
