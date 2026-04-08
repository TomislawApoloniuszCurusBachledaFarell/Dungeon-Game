using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Enums;

namespace Vault_Scavanger.Classes.Utilis;

public class Effect
{
    public StatType Type;
    public int Value;

    public static Effect GetPositiveEffect(StatType type, int value = 2)
    {
        return new Effect { Type = type, Value = value };
    }

    public static Effect GetNegativeEffect(StatType type, int value = 2)
    {
        return new Effect { Type = type, Value = (-1) * value };
    }
}
