using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vault_Scavanger.Classes.Utilis.Rooms;

public class NormalRoom : Room
{
    public NormalRoom(int MaxY, int MaxX, int length = 3, int width = 3, Random rand = null) :
        base(MaxY, MaxX, length, width, rand)
    {

    }

    public override (int, int) GeneratePosition(int Y, int X, Random rand)
    {
        if(rand == null)
        {
            return(Y, X);
        }
        int YPos = rand.Next(1, Y);
        int XPos = rand.Next(1, X);
        return (YPos, XPos);
    }

    public override (int, int) GetCorridorEntrance(Room? other = null, Random? rand = null)
    {
        if(rand == null)
            rand = new Random();
        return(Y + rand.Next(0, Length),  X + rand.Next(0, Width));
    }
}
