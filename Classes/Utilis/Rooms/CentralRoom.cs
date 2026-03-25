using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vault_Scavanger.Classes.Utilis.Rooms;

public class CentralRoom : Room
{
    public CentralRoom(int MaxY, int MaxX, int length = 3, int width = 3, Random random = null) :
    base(MaxY, MaxX, length, width, random)
    {

    }

    public override (int, int) GeneratePosition(int Y, int X, Random random)
    {
        return ((Y - this.Length)/2 ,  (X-this.Width)/2);
    }

    public override (int, int) GetCorridorEntrance(Room? other = null, Random? rand = null)
    {
        if (other == null) 
            return (CenterY,CenterX);

        int YUpDiff =  Math.Abs(Y - (other.Y + other.Length - 1));
        int YDownDiff = Math.Abs(other.Y - (Y + Length - 1));

        int XLeftDiff = Math.Abs(X - (other.X + other.Width - 1));
        int XRightDiff = Math.Abs(other.X - (X + Width - 1));
        
        int min = Math.Min(YUpDiff, YDownDiff);
        min = Math.Min(min, XLeftDiff);
        min = Math.Min(min, XRightDiff);

        if (min == YUpDiff)
            return (Y, CenterX);
        else if (min == YDownDiff)
            return (Y + Length - 1, CenterX);
        else if (min == XLeftDiff)
            return (CenterY, X);
        return (CenterY, X + Width - 1);
    }
}
