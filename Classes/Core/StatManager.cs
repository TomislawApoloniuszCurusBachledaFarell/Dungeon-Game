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
    public List<ActiveEffect> TickingEffects;
    public StatManager()
    {
        Stats = new Dictionary<StatType, Stats>();
        CurrentEffects = new List<ActiveEffect>();
        TickingEffects = new List<ActiveEffect>();
    }

    public void UpdateStats()
    {
        List<ActiveEffect> ExpiredEffects = new List<ActiveEffect>();
        List<ActiveEffect> ExpiredTickingEffects = new List<ActiveEffect>();

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

        foreach (ActiveEffect tickEffect in TickingEffects)
        {
            if(tickEffect.Duration == 0)
            {
                ExpiredTickingEffects.Add(tickEffect);
            }
            else
            {
                ApplyTickEffect(tickEffect);
            }
        }
        foreach(ActiveEffect tickEffect in ExpiredTickingEffects)
        {
            TickingEffects.Remove(tickEffect);
        }
    }
    public void ApplyTickEffect(ActiveEffect tickEffect)
    {
        foreach(Effect effect in tickEffect.effects)
        {
            ModifyStat(effect.Type, effect.Value);
        }
        tickEffect.Duration--;
    }
    public int GetStatMaxValue(StatType type) => Stats[type].MaxValue;
    public int GetStatValue(StatType type) => Stats[type].Value;
    public void AddMaxStatEffect(Effect effect) => ChangeMaxStat(effect.Type, effect.Value);
    public void CancelMaxStatEffect(Effect effect)
    {
        ChangeMaxStat(effect.Type, (-1) * effect.Value);
        Stats[effect.Type].Value = Math.Min(Stats[effect.Type].MaxValue, Stats[effect.Type].Value);
    }
    private void ChangeMaxStat(StatType type, int val)
    {
        Stats[type].MaxValue += val;
    }
    public void ModifyStat(StatType type, int val)
    {
        Stats[type].Value += val;
    }
    public void AddTickEffect(List<Effect> effects, string category, int duration)
    {
        foreach (ActiveEffect tickEffect in TickingEffects)
        {
            if (tickEffect.category == category)
            {
                tickEffect.Duration = duration;
                return;
            }
        }
        TickingEffects.Add(new ActiveEffect { effects = effects, Duration = duration, category = category });
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
            AddEffect(effect);
        }
    }
    public void AddEffect(Effect effect)
    {
        if (Stats[effect.Type].Value + effect.Value > Stats[effect.Type].MaxValue)
            effect.Value = Stats[effect.Type].MaxValue - Stats[effect.Type].Value;
        ModifyStat(effect.Type, effect.Value);
    }
    public void RemoveEffect(Effect effect)
    {
        ModifyStat(effect.Type, (-1) * effect.Value);

    }
    public void RemoveEffect(ActiveEffect source) 
    {
        foreach(Effect effect in source.effects)
            RemoveEffect(effect);
        CurrentEffects.Remove(source);
    }

    public void ImplementStat(StatType statType, int maxValue = 10, int value = -1, bool visible = true)
    {
        if(value == -1)
        {
            value = maxValue / 2;
        }
        Stats.Add(statType, new Stats(statType, maxValue, value, visible));
    }
    
}
