using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Enums;
using Vault_Scavanger.Interfaces.CoreInterfaces;

namespace Vault_Scavanger.Classes.Core.VaultBuilder.Building_Procedures;

public class EmptyVaultProcedure : IBuildProcedure
{
    public Features Execute(VaultBuilder builder)
    {
        for (int y = 1; y < builder.Y - 1; y++)
        {
            for (int x = 1; x < builder.X - 1; x++)
            {
                builder.board[y, x] = ' ';
            }
        }
        return Features.Movement;
    }
}
