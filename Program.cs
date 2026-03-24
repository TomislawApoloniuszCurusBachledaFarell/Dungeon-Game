
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

        MiscellaneousItem ESSBottle = new MiscellaneousItem { Name = "Empty Sunset Sarsaparilla bottle", Symbol = 'E', Value = 2 };
        MiscellaneousItem BigESSBottle = new MiscellaneousItem { Name = "Big Empty Sunset Sarsaparilla bottle", Symbol = 'B', Value = 2, TwoHanded = true };
        MiscellaneousItem Pin = new MiscellaneousItem { Name = "Bobby Pin", Symbol = 'B', Value = 0, TwoHanded = false };

        RangedWeapon BBGun = new RangedWeapon ("BB Gun", 'g', 36, 4, true );
        RangedWeapon TenMmPistol = new RangedWeapon("10mm Pistol", 'P', 250, 22, false);
        MeleeWeapon RollingPin = new MeleeWeapon("Rolling Pin", 'R', 10, 3, false);

        GoldBar goldBar = new GoldBar();
        BottleCap bottleCap = new BottleCap();
        maze.addItem(ESSBottle, 6, 11);
        maze.addItem(TenMmPistol, 11, 6);
        maze.addItem(RollingPin, 14, 10);
        maze.addItem(BBGun, 7, 2);
        maze.addItem(ESSBottle, 6, 11);
        maze.addItem(ESSBottle, 6, 11);
        maze.addItem(ESSBottle, 6, 11);
        maze.addItem(ESSBottle, 6, 11);
        maze.addItem(Pin, 5, 6);
        maze.addItem(Pin, 15, 3);

        maze.addItem(BBGun, 7, 2);

        maze.addItem(BigESSBottle, 7, 4);
        maze.addItem(bottleCap, 2, 2);
        maze.addItem(bottleCap, 4, 4);
        maze.addItem(goldBar, 10, 10);

        GameLoop game = new GameLoop(player, maze);
        game.Run();

        Console.Clear();
        Console.WriteLine("Thanks for playing the game");
    }


}
