using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vault_Scavanger.Classes.Utilis.Rooms;

public record Corridor
{
    public (int Y, int X) From;
    public (int Y, int X) To;
    public Corridor((int Y, int X) from, (int Y, int X) to)
    {
        From = NormalizeFrom(from, to);
        To = NormalizeTo(from, to);
    }

    private static (int Y, int X) NormalizeFrom((int Y, int X) a, (int Y, int X) b)
    {
        if (a.Y < b.Y)
            return a;
        else if(a.Y > b.Y)
            return b;

        if(a.X < b.X)
            return a;
        else if(a.X > b.X)
            return b;
        return a;
    }

    private static (int Y, int X) NormalizeTo((int Y, int X) a, (int Y, int X) b)
    {
        if (a.Y > b.Y)
            return a;
        else if (a.Y < b.Y)
            return b;

        if (a.X > b.X)
            return a;
        else if (a.X < b.X)
            return b;
        return b;
    }
}
