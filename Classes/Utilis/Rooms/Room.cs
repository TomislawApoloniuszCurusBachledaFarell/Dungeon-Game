using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vault_Scavanger.Classes.Utilis.Rooms;

public abstract class Room
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public int CenterX => X + Width / 2;
    public int CenterY => Y + Length / 2;
    public int Width { get; private set; }
    public int Length { get; private set; }

    protected Room(int MaxY, int MaxX, int length = 3, int width = 3, Random random = null)
    {
        Length = length;
        Width = width;
        (int y, int x) pos = GeneratePosition(MaxY, MaxX, random);
        this.X = pos.x;
        this.Y = pos.y;

    }

    public abstract (int, int) GeneratePosition(int Y, int X, Random random);

    public bool Intersects(Room other)
    {
        int margin = 1;
        if (X + Width + margin< other.X)
            return false;
        else if (Y + Length + margin < other.Y)
            return false;
        else if (X > other.X + other.Width + margin) 
            return false;
        else if(Y > other.Y + other.Length + margin)
            return false;

        return true;
    }

    public void MoveRoom(int xShift, int yShift)
    {
        X += xShift; 
        Y += yShift;
    }

    public void MoveAwayFrom(Room other)
    {
        int xDif = this.X - other.X;
        int yDif = this.Y - other.Y;
        if(Math.Abs(xDif) < Math.Abs(yDif))
        {
            if (yDif < 0)
                yDif--;
            else
                yDif++;
            this.MoveRoom(0, yDif);
        }
        else
        {
            if (xDif < 0)
                xDif--;
            else
                xDif++;

            this.MoveRoom(xDif, 0);
        }
    }
}
