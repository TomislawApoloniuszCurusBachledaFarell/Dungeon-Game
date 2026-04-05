using Maze_Mania.Interfaces.ItemInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Items.Equipable;
using Vault_Scavanger.Classes.Items.Equipable.Weapon;
using Vault_Scavanger.Classes.Utilis.Extensions;
using Vault_Scavanger.Classes.Utilis.Factories;
using Vault_Scavanger.Enums;
using Vault_Scavanger.Interfaces.CoreInterfaces;

namespace Vault_Scavanger.Classes.Core.VaultBuilder.Building_Procedures;

public class AddEnemies : IBuildProcedure
{
    int EnemiesCount;
    public AddEnemies(int EnemiesCount)
    {
        this.EnemiesCount = EnemiesCount;
    }

    public void Execute(VaultBuilder builder, ref Features features)
    {
        List<(int Y, int X)> possibleTiles = builder.GetFreeSpaces();

        for (int i = 0; i < EnemiesCount; i++)
        {
            int tile = builder.rand.Next(0, possibleTiles.Count);
            if (possibleTiles.Count == 0) return;
            (int Y, int X) = possibleTiles.GetAndRemove(tile);
            Enemy enemy = EnemyFactory.CreateRandomEnemy(builder.rand, Y, X);
            builder.addEnemy(enemy, possibleTiles[tile]);


            features |= Features.Enemies;
        }

    }

}
