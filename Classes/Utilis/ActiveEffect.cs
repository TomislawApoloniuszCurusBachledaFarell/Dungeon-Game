using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vault_Scavanger.Classes.Utilis;

public class ActiveEffect
{
    public List<Effect> effects;
    public int Duration;
    public string category;

    public static ActiveEffect GetActiveEffect(List<Effect> effects, string category, int duration) =>
        new ActiveEffect { effects = effects, Duration = duration, category = category };
}
