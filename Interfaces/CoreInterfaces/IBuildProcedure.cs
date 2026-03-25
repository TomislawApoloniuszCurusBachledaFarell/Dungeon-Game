using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Core.VaultBuilder;
using Vault_Scavanger.Enums;

namespace Vault_Scavanger.Interfaces.CoreInterfaces;

public interface IBuildProcedure
{
    public Features Execute(VaultBuilder builder);
}
