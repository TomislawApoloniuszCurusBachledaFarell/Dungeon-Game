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
        int YPos = rand.Next(Y);
        int XPos = rand.Next(X);
        return (YPos, XPos);
    }
}
