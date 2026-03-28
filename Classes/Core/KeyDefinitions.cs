using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Enums;

namespace Vault_Scavanger.Classes.Core;

public class KeyDefinitions
{
    Dictionary<GameActions, ConsoleKey> keyBindings;

    public KeyDefinitions() 
    {
        keyBindings = new Dictionary<GameActions, ConsoleKey>();
        keyBindings.Add(GameActions.MoveUp, ConsoleKey.W);
        keyBindings.Add(GameActions.MoveDown, ConsoleKey.S);
        keyBindings.Add(GameActions.MoveLeft, ConsoleKey.A);
        keyBindings.Add(GameActions.MoveRight, ConsoleKey.D);

        keyBindings.Add(GameActions.Drop, ConsoleKey.Q);
        keyBindings.Add(GameActions.Equip, ConsoleKey.F);
        keyBindings.Add(GameActions.PickUp, ConsoleKey.E);
        keyBindings.Add(GameActions.LeftHand, ConsoleKey.L);
        keyBindings.Add(GameActions.RightHand, ConsoleKey.R);

        keyBindings.Add(GameActions.Unequip, ConsoleKey.G);
        keyBindings.Add(GameActions.GameExit, ConsoleKey.X);
        keyBindings.Add(GameActions.CancelAction, ConsoleKey.N);
    }
    
    public ConsoleKey GetActionKey(GameActions action)
    {
        return keyBindings[action];
    }

    public void ChangeBinding(GameActions action, ConsoleKey key) 
    {
        keyBindings[action] = key;
    }
}
