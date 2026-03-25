using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Interfaces.CoreInterfaces;

namespace Vault_Scavanger.Classes.Core.VaultBuilder.Building_Procedures;

public class FilledVaultProcedure : IBuildProcedure
{
    public void Execute(VaultBuilder builder)
    {
        for (int y = 0; y < builder.Y; y++)
        {
            for (int x = 0; x < builder.X; x++)
            {
                builder.board[y, x] = '█';
            }
        }
    }
}
