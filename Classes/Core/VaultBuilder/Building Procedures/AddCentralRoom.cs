using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Utilis.Rooms;
using Vault_Scavanger.Enums;
using Vault_Scavanger.Interfaces.CoreInterfaces;

namespace Vault_Scavanger.Classes.Core.VaultBuilder.Building_Procedures;

public class AddCentralRoom : IBuildProcedure
{
    public int MaxLength;
    public int MaxWidth;
    public int minimalSize = 6;
    public AddCentralRoom(int maxLength, int maxWidth)
    {
        MaxLength = Math.Max(minimalSize, maxLength);
        MaxWidth = Math.Max(minimalSize, maxWidth);
    }

    public void Execute(VaultBuilder builder, ref Features features)
    {
        if (builder.hasCentral) return;
        int Length = builder.rand.Next(minimalSize, MaxLength + 1);
        int Width = builder.rand.Next(minimalSize, MaxWidth + 1);
        if (builder.rooms.Count > 0)
        {
            Room room = builder.rooms.First();
            builder.rooms[0] = new CentralRoom(builder.board.GetLength(0), builder.board.GetLength(1), Length, Width);
            builder.rooms.Add(room);
        }
        else
        {
            builder.rooms.Add(new CentralRoom(builder.board.GetLength(0), builder.board.GetLength(1), Length, Width));
        }
        builder.hasCentral = false;
    }
}
