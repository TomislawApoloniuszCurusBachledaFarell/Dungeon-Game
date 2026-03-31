using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Classes.Core;
using Maze_Mania.Interfaces.ItemInterfaces;
using Vault_Scavanger.Classes.Core.VaultBuilder.Building_Procedures;
using Vault_Scavanger.Classes.Utilis;
using Vault_Scavanger.Classes.Utilis.Rooms;
using Vault_Scavanger.Enums;
using Vault_Scavanger.Interfaces.CoreInterfaces;

namespace Vault_Scavanger.Classes.Core.VaultBuilder;

public class VaultBuilder
{
    public List<Room> rooms;
    public List<Corridor> corridors;
    public int X;
    public int Y;
    public char[,] board;
    public List<IItem>[,] ItemBoard;
    public Random rand;
    public bool hasCentral = false;
    public Features features = Features.None;
    public VaultBuilder(int Y, int X) 
    {
        rooms = new List<Room>();
        corridors = new List<Corridor>();
        board = new char[Y, X];
        ItemBoard = new List<IItem>[Y, X];
        for (int y = 0; y < Y; y++)
        {
            for (int x = 0; x < X; x++)
            {
                ItemBoard[y, x] = new List<IItem>();
            }
        }
        this.X = X;
        this.Y = Y;
        rand = new Random();
    }

    public void Build()
    {
        List<IBuildProcedure> NormalVault = new List<IBuildProcedure>
        {
            new FilledVaultProcedure(),
            //new EmptyVaultProcedure(),
            new AddCentralRoom(8, 15),

            new AddRooms(15, 4, 7),

            new AddCorridors(14),
            new AddItems(12, 6),
            new AddCurrencies(5)
        };
        foreach (IBuildProcedure Procedure in NormalVault) 
        {
            Procedure.Execute(this, ref features);
        }

    }

    public void DrawRooms()
    {
        foreach (Room room in rooms)
        {
            int xSize = room.X;
            int ySize = room.Y;
            int length = room.Length;
            int width = room.Width;

            int xStart = Math.Max(1, xSize);
            int yStart = Math.Max(1, ySize);
            int yMax = Math.Min(Y - 1, yStart + length);
            int xMax = Math.Min(X - 1, xStart + width);
            for (int y = yStart; y < yMax; y++)
            {
                for (int x = xStart; x < xMax; x++)
                {
                    board[y, x] = ' ';

                }
            }
        }
    }

    public void DrawCorridor()
    {
        foreach (Corridor corridor in corridors )
        {
            int fromY = corridor.From.Y;
            int toY = corridor.To.Y;
            int fromX = corridor.From.X;
            int toX = corridor.To.X;
            for(int y = Math.Min(fromY, toY); y <= Math.Max(toY, fromY); y++)
            {
                board[y, fromX] = ' ';
            }
            for(int x = Math.Min(fromX, toX); x <= Math.Max(toX, fromX); x++)
            {
                board[toY, x] = ' ';
            }
        }
    }

    public (int yPos, int xPos) GeneratePlayerPosition()
    {
        for(int y = 0; y < Y; y++)
        {
            for (int x = 0; x < X; x++)
            {
                if(board[y, x] != '█')
                    return (y, x);
            }
        }
        return (0, 0);
    }

    public List<(int Y, int X)> GetFreeSpaces()
    {
        List<(int Y, int X)> result = new List<(int Y, int X)> ();
        for(int i = 0; i < Y; i++)
        {
            for(int j = 0; j < X; j++)
            {
                if (board[i, j] != '█')
                    result.Add((i, j));
            }
        }
        return result;
    }

    public int addItem(IItem item, (int y, int x) Tile)
    {
        if (!isAccesible(Tile.y, Tile.x))
        {
            return 0;
        }
        if (board[Tile.y, Tile.x] == ' ')
            board[Tile.y, Tile.x] = item.Symbol;
        ItemBoard[Tile.y, Tile.x].Add(item);
        return 1;
    }

    private bool isAccesible(int y, int x)
    {
        bool a = x >= 0;
        bool b = y >= 0;
        bool c = x < board.GetLength(1);
        bool d = y < board.GetLength(0);
        if (a && b && c && d)
            return board[y, x] != '█';
        else
            return false;
    }
}
