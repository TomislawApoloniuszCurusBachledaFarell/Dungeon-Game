using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Classes.Core.KeyHandlers;
using Maze_Mania.Enums;

namespace Maze_Mania.Classes.Core;

public class GameLoop
{
    public Player player { get; set; }
    public Maze maze { get; set; }
    public bool isRunning { get; set; }
    private string? message { get; set; }
    public InputMode inputMode = InputMode.Normal;
    private int? tempItemIndex = null;

    public GameLoop(Player player, Maze maze)
    {
        this.player = player;
        this.maze = maze;
        isRunning = true;
        inputHandler = new InputHandler();
    }

    public InputHandler inputHandler { get; set; }

    public void Run()
    {
        GameState gameState;
        gameState = updateGameState();
        Printer.initPrint(gameState);
        while (isRunning)
        {
            Printer.Print(gameState);
            isRunning = ReadInput(Console.ReadKey(true).KeyChar);
            gameState = updateGameState();

            
        }

    }

    private bool ReadInput(char key) => 
        inputHandler.HandleInput(key, player, maze, ref inputMode, ref tempItemIndex);

    private GameState updateGameState()
    {
        List<string>? Interaction = findInteractions();
        GameState state = new GameState(maze.board, player, Interaction, inputMode, "PLACE HOLDER PLACE HOLDER PLACE HOLDER PLACE HOLDER PLACE HOLDER PLACE HOLDER PLACE HOLDER");
        return state;
    }

    private List<string> findInteractions() => InteractionFinder.FindInteractions(maze, player, inputMode);
}
