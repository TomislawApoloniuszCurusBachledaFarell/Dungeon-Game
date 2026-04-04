using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Mania.Classes.Utilis;
using Maze_Mania.Enums;
using Maze_Mania.Interfaces;

namespace Maze_Mania.Classes.Core;

public class GameState
{
    public char[,] board {  get; set; }
    public PlayerState PlayerPos {  get; set; }
    public List<string> itemList {get; set; }
    public Hands hands { get; set; }
    public int BottleCaps;
    public int GoldBars;
    public string message;
    public InputMode Mode;
    public DropOptions DropOptions;
    public List<Stats> stats { get; set; }
    public List<string> Interaction {  get; set; }

    public class PlayerState
    {
        public int X {  get; set; }
        public int Y { get; set; }
        public char Symbol { get; set; }
    }

    public GameState(char[,] board, Player player, List<string> Interaction, InputMode mode, string message)
    {
        this.board = board;
        PlayerPos = new PlayerState();
        PlayerPos.X = player.xPos;
        PlayerPos.Y = player.yPos;
        PlayerPos.Symbol = '@';
        itemList = new List<string>();
        foreach (var item in player.inventory.items) 
        {
            itemList.Add(item.Name);
        }
        hands = player.inventory.hands;
        BottleCaps = player.inventory.currency.BottleCaps;
        GoldBars = player.inventory.currency.GoldBars;
        this.Interaction = Interaction;
        Mode = mode;
        stats = new List<Stats>();
        foreach (Stats entry in player.stats.Stats.Values)
        {
            stats.Add(entry);

        }
        this.message = message;

    }
}
