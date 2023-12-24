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

    public StatType statType;
    public string Name { get; set; }
    public string Description { get; set; }
    public float StatValue { get; set; }

    //this feels ugly, unless you want to load items from a seperate asset(json or sql or w/e), I dont know how else to implement this.
    public Tuple<string, string> GetStatAndDescription(Stat stat)
    {
        switch (stat.statType)
        {
            case StatType.critChance:
            {
                return Tuple.Create("Critical Chance", "Probability of landing a critical hit."); ;
            }
            case StatType.critDamageMultiplier:
            {
                return Tuple.Create("Crit Damage Multiplier", "Factor by which damage is multiplied on a critical hit."); ;
            }
            case StatType.attackSpeed:
            {
                return Tuple.Create("Attack Speed", "Speed at which attacks are performed."); ;
            }
            case StatType.health:
            {
                return Tuple.Create("Health", "Adds to your total health."); ;
            }
            case StatType.mana:
            {
                return Tuple.Create("Mana", "Magical energy for casting spells"); ;
            }
            case StatType.defence:
            {
                return Tuple.Create("Defence", "Resist incoming damage."); ;
            }
            case StatType.spellDamage:
            {
                return Tuple.Create("Spell Damage", "Potency of magical or spell-based attacks"); ;
            }
            case StatType.cooldownReduction:
            {
                return Tuple.Create("Cooldown Reduction", "Reduction in cooldown periods for abilities."); ;
            }
        }
        return null;
    }
}
