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
}
