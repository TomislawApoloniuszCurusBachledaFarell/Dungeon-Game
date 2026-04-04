using Maze_Mania.Classes.Utilis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault_Scavanger.Classes.Utilis;
using Vault_Scavanger.Enums;

namespace Vault_Scavanger.Classes.Core;

public class StatManager
{
    public Dictionary<StatType, Stats> Stats;
    public List<ActiveEffect> CurrentEffects;
    public StatManager(int health = 100, int strength = 10, int agility = 10, int luck = 10, int inteligence = 10, int perception = 10)
    {
        Stats = new Dictionary<StatType, Stats>();
        CurrentEffects = new List<ActiveEffect>();
        Stats.Add(StatType.health, new Stats("Health", health, 1));
        Stats.Add(StatType.strength, new Stats("Strength", strength, 2));

        Stats.Add(StatType.perception, new Stats("Perception", perception, 2));
        Stats.Add(StatType.inteligence, new Stats("Inteligence", inteligence, 2));

        Stats.Add(StatType.agility, new Stats("Agility", agility, 2));
        Stats.Add(StatType.luck, new Stats("Luck", luck, 2));
    }
    
    public void UpdateStats()
    {
        List<ActiveEffect> ExpiredEffects = new List<ActiveEffect>();
        foreach (ActiveEffect effect in CurrentEffects) 
        {
            if (effect.Duration == 0)
                ExpiredEffects.Add(effect);
            else
                effect.Duration--;
        }
        foreach(ActiveEffect effect in ExpiredEffects)
        {
            RemoveEffect(effect);
        }
    }

    public void ModifyStat(StatType type, int val)
    {
        Stats[type].Value += val;
    }
    public void AddEffect(List<Effect> effects, string category, int duration)
    {
        foreach(ActiveEffect currentEffect in CurrentEffects)
        {
            if(currentEffect.category == category)
            {
                currentEffect.Duration = duration;
                return;
            }
        }
        CurrentEffects.Add(new ActiveEffect { effects = effects, Duration = duration, category = category });
        foreach (Effect effect in effects)
        {
            ModifyStat(effect.Type, effect.Value);
        }
    }
    public void AddEffect(List<Effect> effects)
    {
        foreach (Effect effect in effects)
        {
            if (Stats[effect.Type].Value + effect.Value > Stats[effect.Type].MaxValue)
                effect.Value = Stats[effect.Type].MaxValue - Stats[effect.Type].Value;
            ModifyStat(effect.Type, effect.Value);
        }
    }
    public void RemoveEffect(ActiveEffect source) 
    {
        foreach(Effect effect in source.effects)
            ModifyStat(effect.Type, (-1) * effect.Value);
        CurrentEffects.Remove(source);
    }
}
