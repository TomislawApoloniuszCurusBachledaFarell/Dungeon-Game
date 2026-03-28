
using Maze_Mania.Classes.Core;
using Maze_Mania.Classes.Items.Currency;
using Maze_Mania.Classes.Items.Miscellaneous;
using Maze_Mania.Classes.Items.Weapon;
using Maze_Mania.Interfaces;
using Vault_Scavanger.Classes.Core.VaultBuilder;

public static class Program
{
    static void Main()
    {
        
        VaultBuilder vaultBuilder = new VaultBuilder(20, 40);
        vaultBuilder.Build();
        Player player = new Player(vaultBuilder.GeneratePlayerPosition());
        Maze maze = new Maze(player, vaultBuilder);

        GameLoop game = new GameLoop(player, maze, vaultBuilder.features);
        game.Run();

        Console.Clear();
        Console.WriteLine("Thanks for playing the game");
    }


}
