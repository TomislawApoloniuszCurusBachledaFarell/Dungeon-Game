using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Classes.Core.KeyHandlers;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Enums;
using Vault_Scavanger.Enums;

namespace Maze_Mania.Classes.Core;

public class GameLoop
{
    public Player player { get; set; }
    public Maze maze { get; set; }
    public bool isRunning { get; set; }
    private string? inputMessage = "";
    public InputMode inputMode = InputMode.Normal;
    private int? tempItemIndex = null;
    Features features;

    public GameLoop(Player player, Maze maze, Features features)
    {
        this.player = player;
        this.maze = maze;
        this.features = features;
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

    private bool ReadInput(char key)
    {
        InputIResult result = inputHandler.HandleInput(key, player, maze, ref inputMode, ref tempItemIndex);
        inputMessage = result.resultMessage + ".";

        return result.success;
    }

    private GameState updateGameState()
    {
        List<string>? Interaction = findInteractions();
        GameState state = new GameState(maze.board, player, Interaction, inputMode, inputMessage, features);
        return state;
    }

    private List<string> findInteractions() => InteractionFinder.FindInteractions(maze, player, inputMode);
}
