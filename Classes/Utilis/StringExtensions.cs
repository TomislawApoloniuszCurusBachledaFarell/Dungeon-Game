using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Maze_Mania.Classes.Utilis;

public static class StringExtension
{

    public static string PadCentre(this string str,int len)
    {
        StringBuilder sb = new StringBuilder();
        int side = (len - str.Length) / 2;
        if (side < 0) { return str.Substring(0, len); }
        sb.Append(str.PadLeft(side + str.Length));
        side = Math.Max(0, len - (side + str.Length));
        sb.Append("".PadRight(side));
        return sb.ToString();
    }
}
