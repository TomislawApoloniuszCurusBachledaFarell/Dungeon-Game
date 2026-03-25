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

    public abstract (int, int) GetCorridorEntrance(Room? other = null, Random? rand = null);

    public void SetPosition(int y, int x)
    {
        Y = y;
        X = x;
    }

    public bool FitsInside(int maxY, int maxX, int margin = 1)
    {
        return X >= margin &&
               Y >= margin &&
               X + Width <= maxX - margin &&
               Y + Length <= maxY - margin;
    }

    public int DistanceTo(Room other)
    {
        int YUpDiff = Math.Abs(Y - (other.Y + other.Length));
        int YDownDiff = Math.Abs(other.Y - (Y + Length));

        int XLeftDiff = Math.Abs(X - (other.X + other.Width));
        int XRightDiff = Math.Abs(other.X - (X + Width));

        int YDiff = Math.Min(YUpDiff, YDownDiff);
        int XDiff = Math.Min(XLeftDiff, XRightDiff);
        return YDiff + XDiff;
    }

    public bool PointIsInside(int y, int x)
    {
        return Y <= y && Y + Length > y && X <= x && X + Width > x;
    }

    public bool PointIsInside((int y, int x) Point) => PointIsInside(Point.y, Point.x);
}
