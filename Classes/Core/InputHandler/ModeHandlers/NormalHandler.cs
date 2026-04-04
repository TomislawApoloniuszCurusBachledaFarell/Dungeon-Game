using Maze_Mania.Classes.Core;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Enums;
using Maze_Mania.Interfaces.CoreInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Utilis;

namespace Vault_Scavanger.Classes.Core.InputHandler.ModeHandlers;

public abstract class NormalHandler : IModeHandler
{
    protected NormalHandler? Next { get; private set;  }
    public NormalHandler SetNext(NormalHandler next)
    {
        Next = next;
        return next;
    }

    public InputIResult HandleKey(ConsoleKey PressedKey, Player player, Maze maze, KeyDefinitions KeyBinds, ref InputMode inputMode, ref int? tempItemIndex)
    {
        if (canHandle(PressedKey, KeyBinds)) 
        {
            return Process(PressedKey, player, maze, KeyBinds, ref inputMode, ref tempItemIndex);

        }
        else if(Next != null) 
        {
            return Next.HandleKey(PressedKey, player, maze, KeyBinds, ref inputMode, ref tempItemIndex);
            

        }
        else
        {
            return new InputIResult() { resultMessage = InputMessages.NoFunction() , success =  true };
        }

    }

    protected abstract bool canHandle(ConsoleKey PressedKey, KeyDefinitions keyBinds);
    protected abstract InputIResult Process(ConsoleKey ckey, Player player, Maze maze, KeyDefinitions KeyBinds, ref InputMode inputMode, ref int? tempItemIndex);
}
