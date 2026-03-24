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
    int minimalSize = 4;
    public AddRooms(int count, int maxLength = 0, int maxWidth = 0)
    {
        Count = count;
        MaxLength = Math.Max(minimalSize + 2, maxLength);
        MaxWidth = Math.Max(minimalSize + 2, maxWidth);
    }

    public void Execute(VaultBuilder builder)
    {
        for (int i = 0; i < Count; i++)
        {
            int Length = builder.rand.Next(minimalSize, MaxLength + 1);
            int Width = builder.rand.Next(minimalSize, MaxWidth + 1);
            builder.rooms.Add(new NormalRoom(builder.board.GetLength(0), builder.board.GetLength(1), Length, Width, builder.rand));
        }

        NormalizeRooms(builder);
    }

    public void NormalizeRooms(VaultBuilder builder)
    {
        List<Room> notNormalized = new List<Room>(builder.rooms);
        List<Room?> alreadyNormalized = new List<Room>();
        alreadyNormalized.Add(notNormalized.GetAndRemove(0));
        if (alreadyNormalized[0] == null)
            return;
        int iterationCount = notNormalized.Count;
        int safety = 100;
        for (int i = 0; i < iterationCount; i++)
        {
            Room room = notNormalized[0];
            bool normalized = false;
            int curIteration = 0;
            while (!normalized && curIteration < safety)
            {
                curIteration++;
                normalized = true;
                foreach (Room otherRoom in alreadyNormalized)
                {
                    if (!room.Intersects(otherRoom))
                    {
                        continue;
                    }
                    normalized = false;
                    room.MoveAwayFrom(otherRoom);
                    break;
                }
            }


            alreadyNormalized.Add(notNormalized.GetAndRemove(0));
        }
        builder.rooms = alreadyNormalized;
    }
}