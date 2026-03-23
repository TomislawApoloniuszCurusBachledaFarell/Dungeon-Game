using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vault_Scavanger.Classes.Utilis;

public class Room
{
    public int X { get; private set; }
    public int Y { get; private set;}
    public int CenterX => X + Width / 2;
    public int CenterY => Y + Length / 2;
    public int Width { get; private set; }
    public int Length { get; private set; }

    public Room(int Y, int X, int length, int width)
    {
        this.X = X;
        this.Y = Y;
        this.Length = length;
        this.Width = width;
    }
}
