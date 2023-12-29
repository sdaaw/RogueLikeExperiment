using System;
using System.Collections;
using System.Collections.Generic;


public class Stat
{
    public enum StatType
    {
        critChance,
        critDamageMultiplier,
        attackSpeed,
        health,
        mana,
        defence,
        spellDamage,
        cooldownReduction
    }

    private record StatDescriptions
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public StatType Type { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float StatValue { get; set; }

    private StatDescriptions _statDescriptions = new();

    //this feels ugly, unless you want to load items from a seperate asset(json or sql or w/e), I dont know how else to implement this.

    public static Stat CreateStat(Random random)
    {
        Stat stat = new Stat();
        StatType[] types = (StatType[])Enum.GetValues(typeof(StatType));
        int roll = random.Next(1, types.Length);
        stat.Type = types[roll];
        stat.StatValue = random.Next(1, 100);

        // Fill Name & Description
        StatDescriptions desc = stat.GetStatAndDescription();
        stat.Name = desc.Name;
        stat.Description = desc.Description;
        return stat;
    }
 
    private StatDescriptions GetStatAndDescription()
    {
        StatDescriptions descriptions = new StatDescriptions();
        switch (Type)
        {
            case StatType.critChance:
            {
                descriptions.Name = "Critical Chance";
                descriptions.Description = "Probability of landing a critical hit.";
                return descriptions;
            }
            case StatType.critDamageMultiplier:
            {
                descriptions.Name = "Crit Damage Multiplier";
                descriptions.Description = "Factor by which damage is multiplied on a critical hit.";
                return descriptions;
            }
            case StatType.attackSpeed:
            {
                descriptions.Name = "Attack Speed";
                descriptions.Description = "Speed at which attacks are performed.";
                return descriptions;
            }
            case StatType.health:
            {
                descriptions.Name = "Health";
                descriptions.Description = "Adds to your total health.";
                return descriptions;
            }
            case StatType.mana:
            {
                descriptions.Name = "Mana";
                descriptions.Description = "Magical energy for casting spells";
                return descriptions;
            }
            case StatType.defence:
            {
                descriptions.Name = "Defence";
                descriptions.Description = "Resist incoming damage";
                return descriptions;
            }
            case StatType.spellDamage:
            {
                descriptions.Name = "Spell Damage";
                descriptions.Description = "Potency of magical or spell-based attacks";
                return descriptions;
            }
            case StatType.cooldownReduction:
            {
                descriptions.Name = "Cooldown Reduction";
                descriptions.Description = "Reduction in cooldown periods for abilities.";
                return descriptions;
            }
        }
        return descriptions;
    }
}
