using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Enums;
using Microsoft.VisualBasic;

namespace Maze_Mania.Classes.Core;

public static class Printer
{
    public static void initPrint(GameState gameState)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.CursorVisible = false;
        Console.WriteLine("For the best experience, please switch the game to fullscreen.");
        Console.WriteLine("Press any key to continue...");
        
        Console.ReadKey(true);
        Console.Clear();
        //Print(gameState);
    }

    public static void Print(GameState gameState)
    {
        int WindowWidth = Console.WindowWidth;
        int WindowHeight = Console.WindowHeight;
        int height = gameState.board.GetLength(0);
        int width = gameState.board.GetLength(1);
        int UpperPadding = (WindowHeight - height)/2;
        int SidePanelWidth = (WindowWidth - width - 2)/2;
        StringBuilder sb = new StringBuilder();
        List<string> leftPannel = new List<string>();
        List<string> rightPannel = new List<string>();
        rightPannel = MakeRightPannel(gameState.itemList, gameState.hands, gameState.BottleCaps,gameState.GoldBars, SidePanelWidth, height);
        leftPannel = MakeLeftPannel(gameState.Interaction, gameState.stats, SidePanelWidth, height);
        
        sb.Append(new String('=', WindowWidth));
        for (int i = 0; i < height; i++)
        {
            sb.Append(leftPannel[i]);
            sb.Append(' ');

            for (int j = 0; j < width; j++)
            {
                if (gameState.PlayerPos.Y != i || gameState.PlayerPos.X != j)
                {
                    sb.Append(gameState.board[i, j]);
                }
                else
                {
                    //Console.Write();
                    sb.Append(gameState.PlayerPos.Symbol);
                }
            }

            sb.Append(' ');
            sb.Append(rightPannel[i]);
            sb.AppendLine();
        }

        Console.SetCursorPosition(0, UpperPadding - 1);
        sb.Append(new String('=', WindowWidth));
        Console.Write(sb.ToString());
        sb.Clear();

    }

    private static List<string> MakeLines(List<string> strings, int width)
    {
        var lines = new List<string>();
        if (strings == null) return lines;
        int i = 0;
        string st;
        foreach (string s in strings)
        {
            st = $"{++i}. " + s;
            if(st.Length <= width)
            {
                lines.Add(st.PadRight(width));
                continue;
            }

            string[] words = st.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            StringBuilder line = new StringBuilder();
            int leftWidth = width;
            foreach(string word in words)
            {
                if (word.Length < leftWidth) 
                {
                    line.Append(word + ' ');
                    leftWidth -= word.Length + 1;
                }
                else
                {
                    lines.Add(line.ToString().PadRight(width));
                    leftWidth = width;
                    line.Clear();
                    line.Append(word + ' ');
                    leftWidth -= word.Length + 1;
                }
            }
            lines.Add(line.ToString().PadRight(width));
            line.Clear();
        }
        return lines;
    }
    private static List<string> MakeLeftPannel(List<string> Interactions, List<Stats> stats, int width, int height)
    {
        List<string> lines = new List<string>();
        lines.Add("Possible interactions".PadCentre(width));
        lines.AddRange(MakeLines(Interactions, width));
        int i = lines.Count;
        for (; i < 12; i++)
        {
            lines.Add($"".PadRight(width));
        }
        lines.Add(new string('=', width));
        lines.Add("Player Information".PadCentre(width));
        foreach(var entry in stats)
        {
            lines.Add(entry.ToString().PadRight(width));
            i++;
        }
        for(; i < height; i++)
        {
            lines.Add(new string(' ', width));
        }
        return lines;
        }
    private static List<string> MakeRightPannel(List<string> items, Hands hands, int BottleCaps, int GoldBars, int width, int height) 
    {
        List<string> lines = new List<string>();
        lines.Add("Inventory".PadCentre(width));
        lines.Add($"Bottle Caps: {BottleCaps}, Gold Bars: {GoldBars}".PadCentre(width));
        int i = 0;
        for (; i < items.Count; i++) 
        {
            lines.Add(($"{i }. " + items[i]).PadRight(width));
        }
        for(; i < 10; i++)
        {
            lines.Add(($"{i }. ").PadRight(width));
        }
        lines.Add(new string('=', width));
        lines.Add("Equipped items".PadCentre(width));
        i += 2;
        lines.Add("Left Hand:".PadRight(width));
        if (hands.isOccupied[0])
            lines.Add(hands.itemSlot[0].Name.PadCentre(width));
        else
            lines.Add("empty".PadCentre(width));
        i += 2;
        lines.Add("Right Hand:".PadRight(width));
        if (hands.isOccupied[1])
            lines.Add(hands.itemSlot[1].Name.PadCentre(width));
        else
            lines.Add("empty".PadCentre(width));

        i += 2;
        for (; i < height; i++)
        {
            lines.Add(new string(' ', width));
        }
        return lines;
    }
}

