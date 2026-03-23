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
        Console.WriteLine("For the best experience, please switch the game to fullscreen and do not change the window size.");
        Console.WriteLine("Press any key to continue...");
        
        Console.ReadKey(true);
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        PrintTitle();
        //Print(gameState);
    }

    public static void PrintTitle()
    {
        int WindowWidth = Console.WindowWidth;
        int WindowHeight = Console.WindowHeight;
        List<string> lines = new List<string>()
        {
            " _    __            ____                             ".PadCentre(WindowWidth),
            "| |  / /___ ___  __/ / /_                            ".PadCentre(WindowWidth),
            "| | / / __ `/ / / / / __/                            ".PadCentre(WindowWidth),
            "| |/ / /_/ / /_/ / / /_                              ".PadCentre(WindowWidth),
            "|___/\\__,_/\\__,_/_/\\__/                              ".PadCentre(WindowWidth),
            "  / ___/_________ __   ______ _____  ____ ____  _____".PadCentre(WindowWidth),
            "  \\__ \\/ ___/ __ `/ | / / __ `/ __ \\/ __ `/ _ \\/ ___/".PadCentre(WindowWidth),
            " ___/ / /__/ /_/ /| |/ / /_/ / / / / /_/ /  __/ /    ".PadCentre(WindowWidth),
            "/____/\\___/\\__,_/ |___/\\__,_/_/ /_/\\__, /\\___/_/     ".PadCentre(WindowWidth),
            "                                  /____/             ".PadCentre(WindowWidth),
        };
        StringBuilder sb = new StringBuilder();
        foreach (string line in lines) 
        {
            sb.AppendLine(line);
        }
        Console.Write(sb.ToString());
    }

      
    public static void Print(GameState gameState)
    {
        //Console.ForegroundColor = ConsoleColor.DarkGreen;

        int WindowWidth = Console.WindowWidth;
        int WindowHeight = Console.WindowHeight;
        int height = gameState.board.GetLength(0);
        int width = gameState.board.GetLength(1);
        int UpperPadding = 11;
        int SidePanelWidth = (WindowWidth - width - 2)/2;
        StringBuilder sb = new StringBuilder();
        List<string> leftPannel = MakeLeftPannel(gameState.Interaction, gameState.stats, SidePanelWidth, height);

        height = leftPannel.Count;
        List<string> messageBox = MakeMessageBox(gameState.message, leftPannel.Count, gameState.board.GetLength(0), gameState.board.GetLength(1));

        List<string> rightPannel = MakeRightPannel(gameState.itemList, gameState.hands, gameState.BottleCaps,gameState.GoldBars, SidePanelWidth, height);
        List<string> centrePannel = MakeCentrePannel(gameState);

        //sb.Append(new String('=', WindowWidth));
        for (int i = 0; i < leftPannel.Count; i++)
        {
            sb.Append(leftPannel[i]);
            sb.Append(' ');
            if( i < centrePannel.Count )
            {
                sb.Append(centrePannel[i]);
            }
            else
            {
                sb.Append(messageBox[i - centrePannel.Count]);
            }

            sb.Append(' ');
            sb.Append(rightPannel[i]);
            sb.AppendLine();
        }

        Console.SetCursorPosition(0, UpperPadding);
        sb.Append(new String('=', WindowWidth));
        Console.Write(sb.ToString());
        sb.Clear();

    }

    private static List<string> MakeLines(List<string> strings, int width, bool numbering = false, TextAlignment textAlignment = TextAlignment.Centre)
    {
        var lines = new List<string>();
        if (strings == null) return lines;
        int i = 0;
        string st;
        foreach (string s in strings)
        {
            st = s;
            if (numbering)
            {
                st = $"{++i}. " + st;
            }
            if (st.Length <= width)
            {
                lines.Add(st.AlignText(width, textAlignment));
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
                    lines.Add(line.ToString().AlignText(width, textAlignment));
                    leftWidth = width;
                    line.Clear();
                    line.Append(word + ' ');
                    leftWidth -= word.Length + 1;
                }
            }
            lines.Add(line.ToString().AlignText(width, textAlignment));
            line.Clear();
        }
        return lines;
    }
    private static List<string> MakeLeftPannel(List<string> Interactions, List<Stats> stats, int width, int height)
    {

        List<string> lines = new List<string>();
        lines.Add(new string('=', width));
        lines.Add("Player Information".PadCentre(width));
        foreach (var entry in stats)
        {
            lines.Add(entry.ToString().PadRight(width));
        }
        lines.Add(new String('=', width));

        lines.Add("Possible interactions".PadCentre(width));
        lines.AddRange(MakeLines(Interactions, width, true, TextAlignment.Left));
        int i = Interactions.Count + 2;
        for (; i < 15; i++)
        {
            lines.Add($" ".PadRight(width));
        }

        return lines;
        }
    private static List<string> MakeCentrePannel(GameState gameState)
    {
        List<string> lines = new List<string>();
        StringBuilder sb = new StringBuilder();
        int height = gameState.board.GetLength(0);
        int width = gameState.board.GetLength(1);
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (gameState.PlayerPos.Y != i || gameState.PlayerPos.X != j)
                {
                    sb.Append(gameState.board[i, j]);
                }
                else
                {
                    sb.Append(gameState.PlayerPos.Symbol);
                }
            }
            lines.Add(sb.ToString());
            sb.Clear();

        }
        return lines;
    }
    private static List<string> MakeRightPannel(List<string> items, Hands hands, int BottleCaps, int GoldBars, int width, int height) 
    {
        List<string> lines = new List<string>();
        lines.Add(new String('=', width));

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
    private static List<string> MakeMessageBox(string message, int SidePannelHeight, int boardHeight, int boardWidth)
    {
        int height = SidePannelHeight - boardHeight;
        List<string> lines = new List<string>();
        
        StringBuilder sb = new StringBuilder();
        List<string> Message = MakeLines(new List<string>() { message }, boardWidth - 2);

        for (int i = 0; i < height; i++)
        {
            string msg;
            if (i < Message.Count)
            {
                msg = Message[i];
            }
            else
            {
                msg = new string(' ', boardWidth -  2);
            }
            lines.Add("|" + msg + "|");
        }
        return lines;
    }
}

