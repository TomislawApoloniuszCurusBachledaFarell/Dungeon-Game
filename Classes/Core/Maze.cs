using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Interfaces.ItemInterfaces;
using Vault_Scavanger.Classes.Core.InputHandler.ModeHandlers;
using Vault_Scavanger.Classes.Core.VaultBuilder;

namespace Maze_Mania.Classes.Core;

public class Maze
{
    public char[,] board { get; }
    public  List<IItem>[,] ItemBoard { get; private set; }
    public Player _player;
    public int X { get; private set; }
    public int Y { get; private set; }

    public Maze(Player player, VaultBuilder builder)
    {
        board = builder.board;
        ItemBoard = builder.ItemBoard;
        X = builder.X;
        Y = builder.Y;

        _player = player;
    }

    public InputIResult PickUp()
    {
        InputIResult result = new InputIResult();
        int x = _player.xPos;
        int y = _player.yPos;

        if (ItemBoard[y, x].Count == 0) 
        {
            result.resultMessage = InputMessages.NoItemToPickUp();
            result.success = true;
            return result;
        }
        IItem? item = ItemBoard[y, x].FirstOrDefault();
        if (_player.hasInventorySpace() || (item != null && item.CanPickUpWhenInventoryFull))
        {
            return _player.pickUpItem(retrieveItem(y, x));
        }
        else
        {
            result.resultMessage = InputMessages.FullInventory();
        }
        result.success = true; 
        return result;
    }

    public InputIResult MoveLeft()
    {
        InputIResult result = new InputIResult();
        int x = _player.xPos;
        int y = _player.yPos;
        if (isAccesible(y, x - 1))
        {
            _player.setPos(x - 1, y);
            result.resultMessage = InputMessages.MoveLeftSuccess(y, x);
            result.success = true ;
            return result;
        }
        result.resultMessage = InputMessages.MoveLeftFailure();
        result.success = true;
        return result;
    }

    public InputIResult MoveRight()
    {
        InputIResult result = new InputIResult();
        int x = _player.xPos;
        int y = _player.yPos;
        if (isAccesible(y, x + 1))
        {
            _player.setPos(x + 1, y);
            result.resultMessage = InputMessages.MoveRightSuccess(y, x);
            result.success = true;
            return result;
        }
        result.resultMessage = InputMessages.MoveRightFailure();
        result.success = true;
        return result;
    }

    public InputIResult MoveUp()
    {
        InputIResult result = new InputIResult();
        int x = _player.xPos;
        int y = _player.yPos;
        if (isAccesible(y - 1, x))
        {
            _player.setPos(x, y - 1);
            result.resultMessage = InputMessages.MoveUpSuccess(y, x);
            result.success = true;
            return result;
        }
        result.resultMessage = InputMessages.MoveUpFailure();
        result.success = true;
        return result;
    }

    public InputIResult MoveDown()
    {
        InputIResult result = new InputIResult();
        int x = _player.xPos;
        int y = _player.yPos;
        if (isAccesible(y + 1, x))
        {
            _player.setPos(x, y + 1);
            result.resultMessage = InputMessages.MoveDownSuccess(y, x);
            result.success = true;
            return result;
        }
        result.resultMessage = InputMessages.MoveDownFailure();
        result.success = true;
        return result;
    }

    private bool isAccesible(int y, int x)
    {
        bool a = x >= 0;
        bool b = y >= 0;
        bool c = x < board.GetLength(1);
        bool e = y < board.GetLength(0);
        bool d;
        if( a && b && c && e)
            d = board[y, x] != '█';
        else 
            return false;

        return x >= 0 && y >= 0 && y < board.GetLength(0) && x < board.GetLength(1) && board[y, x] != '█';
    }

    public int addItem(IItem item, int y, int x)
    {
        if (!isAccesible(y, x))
        {
            return 0;
        }
        if (board[y, x] == ' ')
            board[y, x] = item.Symbol;
        ItemBoard[y, x].Add(item);
        return 1;
    }

    public IItem? retrieveItem(int y, int x)
    {
        if (!isAccesible(y, x))
        {
            return null;
        }
        board[y, x] = ' ';
        IItem item = ItemBoard[y, x].First();
        ItemBoard[y, x].RemoveAt(0);
        if(ItemBoard[y, x].Count() > 0)
        {
            board[y, x] = ItemBoard[y, x].First().Symbol;
        }
        return item;
    }

    public char getChar(int x, int y)
    {
        if (ItemBoard[y, x] == null) return board[y, x];
        return ItemBoard[y, x].First().Symbol;
    }

    public InputIResult PlayerDrops(char c, int index)
    {
        switch (c)
        {
            case 'g':
            case 'c':
                return PlayerDropsCurrency(c);
            default:
                return PlayerDropsItem(index);
        }
    }

    private InputIResult PlayerDropsCurrency(char c)
    {
        IItem? item = _player.dropCurrency(c);
        InputIResult result = new InputIResult();
        /*
        if (item == null)
        {
            result.success = true;
            result.resultMessage = $"Player was unable to drop an item.Index {c} does not contain any item";
            return result;
        }
        */
        result.success = true;
        result.resultMessage = $"{item.Name} was dropped at {_player.yPos}, { _player.xPos}";
        addItem(item, _player.yPos, _player.xPos);
        return result;
    }

    private InputIResult PlayerDropsItem(int index)
    {
        IItem? item = _player.dropItem(index);
        InputIResult result = new InputIResult();

        if (item == null)
        {
            result.success = true;
            result.resultMessage = $"Player was unable to drop an item.Index {index} does not contain any item";
            return result;
        }
        addItem(item, _player.yPos, _player.xPos);
        result.success = true;
        result.resultMessage = $"{item.Name} was dropped at {_player.yPos}, {_player.xPos}";
        return result;
    }
}
