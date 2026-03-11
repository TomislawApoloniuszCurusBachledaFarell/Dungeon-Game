using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze_Mania.Classes.Utilis;

public class Stats
{
    public string Name;
    public int Value;
    public int MaxValue;
    private int BarLength = 10;
    private int MaxNameLen = 12;

    public Stats(string name, int maxValue, int valueToMaxRatio = 1)
    {
        Name = name;
        Value = maxValue/valueToMaxRatio;
        MaxValue = maxValue;
    }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        string nameToPrint = Name;
        if (nameToPrint.Length > MaxNameLen) 
        {
            nameToPrint.Substring(0, MaxNameLen);
        }
        sb.Append($"{nameToPrint}".PadRight(MaxNameLen) + ": ");
        if(MaxValue > 0)
        {
            int colouredBar = (int)Math.Ceiling((double)BarLength * ((double)Value / (double)MaxValue));
            sb.Append(new string('■', colouredBar));
            sb.Append(new string('□', BarLength - colouredBar));
        }
        sb.Append($" {Value}/{MaxValue}");
        return sb.ToString();
    }
}
