using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }

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
        message = null;
        key = Char.ToLower(key);
        switch (inputMode)
        {
            case InputMode.Normal:
                switch (key)
                {
                    case 'a':
                        maze.MoveLeft();
                        break;
                    case 'd':
                        maze.MoveRight();
                        break;
                    case 'w':
                        maze.MoveUp();
                        break;
                    case 's':
                        maze.MoveDown();
                        break;
                    case 'e':
                        if (player.hasInventorySpace())
                            maze.PickUp();
                        break;
                    case 'g':
                        if ((player.isLeftHandOccupied() || player.isRightHandOccupied()) && player.hasInventorySpace())
                            if (player.isLeftHandOccupied() && player.isRightHandOccupied() && !player.IsTwoHandedEquipped())
                                inputMode = InputMode.HandUnequipSelection;
                            else
                            {
                                player.Unequip('l');
                                player.Unequip('r');
                            }
                        break;
                    case 'f':
                        if (player.HasEquipable())
                            inputMode = InputMode.Equip;
                        break;
                    case 'q':
                        if (isDropPossible() != DropOptions.None)
                            inputMode = InputMode.Drop;
                        break;
                    case 'x':
                        return false;
                    default:
                        break;
                }
                break;
            case InputMode.Drop:
                if (char.IsDigit(key))
                {
                    int num = key - '0';
                    if (maze.PlayerDrops(key, num))
                    {
                        inputMode = InputMode.Normal;
                    }
                }
                else
                {
                    switch (key)
                    {
                        case 'n':
                            inputMode = InputMode.Normal;
                            break;
                        case 'c':
                        case 'g':
                            if (maze.PlayerDrops(key, -1))
                                inputMode = InputMode.Normal;
                            break;
                        default:
                            break;
                    }
                }
                break;
            case InputMode.Equip:
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
                break;
            case InputMode.HandSelection:
                switch (key)
                {
                    case 'l':
                    case 'r':
                        if (player.Equip(tempItemIndex, key))
                        {
                            inputMode = InputMode.Normal;
                            tempItemIndex = null;
                        }
                        break;
                    case 'n':
                        inputMode = InputMode.Equip;
                        break;
                    default:
                        break;
                }
                break;
            case InputMode.HandUnequipSelection:
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
                break;
        }

        return true;
    }
    private GameState updateGameState()
    {
        List<string>? Interaction = findInteractions();
        GameState state = new GameState(maze.board, player, Interaction, inputMode);
        return state;
    }

    private List<string> findInteractions()
    {
        List<string> Interactions = new List<string>();
        DropOptions dropOption = isDropPossible();

        switch (inputMode)
        {
            case InputMode.Normal:
                if (player.hasInventorySpace() && maze.board[player.yPos, player.xPos] != ' ')
                    Interactions.Add($"press E to pick up {maze.ItemBoard[player.yPos, player.xPos].First().Name}");
                if (dropOption != DropOptions.None)
                {
                    if (player.HasEquipable())
                        Interactions.Add($"press F to equip an item from your inventory");
                    Interactions.Add($"press Q to drop an item from your inventory");
                }
                if ((player.isLeftHandOccupied() || player.isRightHandOccupied()) && player.hasInventorySpace())
                    Interactions.Add($"press G to unequip an item from your hand");

                Interactions.Add($"press X to exit the game");
                break;
            case InputMode.Drop:

                if ((dropOption & DropOptions.Item) != 0)
                {
                    List<string> availableItemss = player.getAllItemNames();
                    int i = 0;
                    foreach (string availableItem in availableItemss)
                    {
                        Interactions.Add($"press {i} to drop {availableItem}");
                        i++;
                    }

                }
                if ((dropOption & DropOptions.BottleCap) != 0)
                    Interactions.Add($"press C to drop a bottle cap");
                if ((dropOption & DropOptions.GoldBar) != 0)
                    Interactions.Add($"press G to drop a gold bar");
                Interactions.Add("press N to cancel");
                break;
            case InputMode.Equip:
                List<string> availableItems = player.getAllItemNames();
                List<bool> isItemTwoHanded = player.getAllItemHandability();
                for (int i = 0; i < availableItems.Count; i++)
                {

                    if (player.inventory.MaxCapacity == availableItems.Count && isItemTwoHanded[i] && player.isLeftHandOccupied() && player.isRightHandOccupied())
                        continue;
                    Interactions.Add($"press {i} to equip {availableItems[i]}");
                }

                Interactions.Add("press N to cancel");
                break;
            case InputMode.HandSelection:
                Interactions.Add("press R to equip it to your right hand");
                Interactions.Add("press L to equip it to your left hand");

                Interactions.Add("press N to cancel");
                break;
            case InputMode.HandUnequipSelection:
                if (player.isLeftHandOccupied())
                    Interactions.Add("press L to unequip item from your left hand");

                if (player.isRightHandOccupied())
                    Interactions.Add("press R to unequip item from your right hand");

                Interactions.Add("press N to cancel");
                break;
        };


        return Interactions;
    }

    private DropOptions isDropPossible()
    {
        DropOptions dropOptions = new DropOptions();
        if (player.inventory.items.Count > 0)
            dropOptions = DropOptions.Item;
        if (player.inventory.areBottleCaps())
            dropOptions = dropOptions | DropOptions.BottleCap;
        if (player.inventory.areGoldBars())
            dropOptions = dropOptions | DropOptions.GoldBar;
        return dropOptions;
    }
}
