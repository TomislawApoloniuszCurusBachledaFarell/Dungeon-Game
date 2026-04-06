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
    public StatManager()
    {
        Stats = new Dictionary<StatType, Stats>();
        CurrentEffects = new List<ActiveEffect>();
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


    public void AddHealth(int health = 100)
    {
        Stats.Add(StatType.health, new Stats("Health", health, health));
    }
    public void AddStrength(int maxStrength = 10, int strenght = -1)
    {
        if (strenght == -1) strenght = maxStrength / 2;
        Stats.Add(StatType.strength, new Stats("Strength", maxStrength, strenght));
    }
    public void AddPerception(int maxPerception = 10, int perception = -1)
    {
        if (perception == -1) perception = maxPerception / 2;
        Stats.Add(StatType.perception, new Stats("Perception", maxPerception, perception));
    }
    public void AddInteligence(int maxInteligence = 10, int inteligence = -1)
    {
        if (inteligence == -1) inteligence = maxInteligence / 2;
        Stats.Add(StatType.inteligence, new Stats("Inteligence", maxInteligence, inteligence));
    }
    public void AddAgility(int maxAgility = 10, int Agility = -1)
    {
        if (Agility == -1) Agility = maxAgility / 2;
        Stats.Add(StatType.agility, new Stats("Agility", maxAgility, Agility));
    }
    public void AddLuck(int maxLuck = 10, int Luck = -1)
    {
        if (Luck == -1) Luck = maxLuck / 2;
        Stats.Add(StatType.luck, new Stats("Luck", maxLuck, Luck));
    }
    public void AddArmour(int maxArmour = 10, int Armour = -1)
    {
        if (Armour == -1) Armour = maxArmour / 2;
        Stats.Add(StatType.armour, new Stats("Armour", maxArmour, Armour));
    }
}
