using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Utilis;
using Vault_Scavanger.Classes.Utilis.Rooms;
using Vault_Scavanger.Interfaces.CoreInterfaces;

namespace Vault_Scavanger.Classes.Core.VaultBuilder.Building_Procedures;

public class AddRooms : IBuildProcedure
{
    int MaxWidth;
    int MaxLength;
    int Count;
    int minimalSize = 3;
    bool IncludeNonSeparated;
    public AddRooms(int count, int maxLength = 0, int maxWidth = 0, bool IncludeNonSeparated = false)
    {
        Count = count;
        MaxLength = Math.Max(minimalSize, maxLength);
        MaxWidth = Math.Max(minimalSize, maxWidth);
        this.IncludeNonSeparated = IncludeNonSeparated;
    }

    public void Execute(VaultBuilder builder)
    {
        for (int i = 0; i < Count; i++) 
        {
            int width = builder.rand.Next(minimalSize, MaxWidth);
            int length = builder.rand.Next(minimalSize, MaxLength);

            NormalRoom room = new NormalRoom(1, 1, length, width);

            if(TryFitIntoSpiral(builder, room))
            {
                builder.rooms.Add(room);
            }
            else if (IncludeNonSeparated)
            {
                if(room.FitsInside(builder.Y, builder.X)) 
                    builder.rooms.Add(room);
                else
                {
                    builder.rooms.Add(new NormalRoom(builder.rand.Next(builder.Y), builder.rand.Next(builder.X), length, width));
                }
            }
        }

        builder.DrawRooms();
    }

    bool TryFitIntoSpiral(VaultBuilder builder, Room room)
    {
        int startX = builder.rand.Next(2 * builder.X / 5 , 3 * builder.X / 5 );
        int startY = builder.rand.Next(2 * builder.Y / 5 , 3 * builder.Y / 5 );
        int r = Math.Max(builder.X, builder.Y);
        foreach ((int Y, int X) in GeneraateSpiralPosition(startX, startY, r, builder.rand)) 
        {
            room.SetPosition(Y, X);
            if(!room.FitsInside(builder.Y, builder.X))
                continue;
            bool Separate = true;
            foreach (Room otherRoom in builder.rooms) 
            {
                if (room.Intersects(otherRoom))
                {
                    Separate = false;
                    break;
                }
            }
            if (Separate)
                return Separate;
        }
        return false;

    }

    IEnumerable<(int Y, int X)> GeneraateSpiralPosition(int startX, int startY, int maxRadius, Random rand)
    {
        List<(int Y, int X)> points = new List<(int Y, int X)>();

        points.Add((startY, startX));
        for (int r = 1; r <= maxRadius; r++)
        {
            for (int x = startX - r; x <= startX + r; x++)
                points.Add((startY - r, x));

            for (int y = startY - r + 1; y <= startY + r; y++)
                points.Add ((y, startX + r));

            for (int x = startX + r - 1; x >= startX - r; x--)
                points.Add((startY + r, x));

            for (int y = startY + r - 1; y >= startY - r + 1; y--)
                points.Add(((y, startX - r)));
        }

        while(points.Count > 0)
        {
            int index = rand.Next(points.Count);
            yield return points.GetAndRemove(index);
        }
    }
}