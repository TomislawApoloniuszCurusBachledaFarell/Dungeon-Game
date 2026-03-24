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
            new AddCentralRoom(8, 20),
            new AddRooms(3)
        };
        foreach (IBuildProcedure Procedure in NormalVault) 
        {
            Procedure.Execute(this);
        }

        GenerateMap();
    }

    public void GenerateMap()
    {
        foreach (Room room in rooms)
        {
            int xStart = room.X;
            int yStart = room.Y;
            int length = room.Length;
            int width = room.Width;

            int yMax = Math.Min(Y - 1, yStart + length);
            int xMax = Math.Min(X - 1, xStart + width);
            for (int y = yStart; y < yMax; y++) 
            {
                for(int x = xStart; x < xMax; x++)
                {
                    board[y, x] = ' ';

                }
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
}
