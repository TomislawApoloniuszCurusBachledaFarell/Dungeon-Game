using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Classes.Core.KeyHandlers;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Enums;
using Vault_Scavanger.Classes.Core;
using Vault_Scavanger.Classes.Core.InteractionsBuilder;
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
    public Features GameFeatures = Features.None;
    public KeyDefinitions KeyBindings;
    public InteractionFinder interactionFinder;

    public GameLoop(Player player, Maze maze, Features GameFeatures)
    {
        this.player = player;
        this.maze = maze;
        isRunning = true;
        this.GameFeatures = GameFeatures;
        inputHandler = new InputHandler();
        KeyBindings = new KeyDefinitions();
        interactionFinder = new InteractionFinder(KeyBindings);
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
            isRunning = ReadInput(Console.ReadKey(true));
            gameState = updateGameState();

            
        }

    }

    private bool ReadInput(ConsoleKeyInfo key)
    {
        InputIResult result = inputHandler.HandleInput(key.Key, player, maze, KeyBindings, ref inputMode, ref tempItemIndex);
        inputMessage = result.resultMessage + ".";

        return result.success;
    }

    private GameState updateGameState()
    {
        if(inputMode == InputMode.Normal)
        {
            player.UpdatePlayer();
        }
        List<string>? Interaction = findInteractions();
        GameState state = new GameState(maze, player, Interaction, inputMode, inputMessage);
        return state;
    }

    private List<string> findInteractions() => interactionFinder.FindInteractions(maze, player, inputMode);
}
