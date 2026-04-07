using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Enums;

namespace Maze_Mania.Classes.Utilis;

public class Stats
{
    public string Name;
    public int Value;
    public int MaxValue;
    public bool Visible;
    public int GetValue => Math.Min(Value, MaxValue);
    private int BarLength = 10;
    private int MaxNameLen = 12;

    public Stats(StatType type, int maxValue, int value, bool visible = true)
    {
        Name = GetStatTypeName(type);
        Value = value;
        MaxValue = maxValue;
        this.Visible = visible;
    }
    public override string? ToString()
    {
        if(!Visible) return null;
        StringBuilder sb = new StringBuilder();
        string nameToPrint = Name;
        if (nameToPrint.Length > MaxNameLen) 
        {
            nameToPrint.Substring(0, MaxNameLen);
        }
        sb.Append($"{nameToPrint}".PadRight(MaxNameLen) + ": ");
        if(MaxValue > 0)
        {
            int colouredBar = (int)Math.Ceiling((double)BarLength * ((double)GetValue / (double)MaxValue));
            if (colouredBar < 0) colouredBar = 0;
            sb.Append(new string('■', colouredBar));
            sb.Append(new string('□', BarLength - colouredBar));
        }
        sb.Append($" {GetValue}/{MaxValue}");
        return sb.ToString();
    }

    public static string GetStatTypeName(StatType type)
    { 
        switch (type) 
        {
            case StatType.health:
                return "Health";
            case StatType.perception:
                return "Perception";
            case StatType.baseDmg:
                return "BaseDmg";
            case StatType.armour:
                return "Armour";
            case StatType.luck:
                return "Luck";
            case StatType.agility:
                return "Agility";
            case StatType.inteligence:
                return "Inteligence";
            case StatType.strength:
                return "Strength";
            default:
                return "Unknown";
        }
    }
}
