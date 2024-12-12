using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class CharacterData : ScriptableObject
{
    public string Name = "NameLess";
    public CharacterAttributes attributes;
    public CharacterAttributes lvlUpRates;
    public Level level;

    public Stats stats;

    public int GetDefensiveValue(AttackType dt)
    {
        int def = 0;
        switch (dt)
        {
            case AttackType.Physical:
                def += attributes.defence;
                break;
            case AttackType.Magical:
                def += attributes.resistance;
                break;
        }
        return def;
    }

    public int GetDamage(AttackType damageType)
    {
        int damage = 0;
        switch (damageType)
        {
            case AttackType.Physical:
                damage += attributes.strength;
                break;
            case (AttackType.Magical):
                damage += attributes.magic;
                break;
        }
        return damage;
    }

    public void AddExp(int ex)
    {
        level.AddExp(ex);
        if (level.CheckLvlUp())
        {
            LvlUp();
        }
    }

    private void LvlUp()
    {
        level.LvlUp();
        LVLUpAttributes();

    }

    private void LVLUpAttributes()
    {
        for (int i = 0; i < CharacterAttributes.AttributeCount; i++)
        {
            int rate = lvlUpRates.Get((CharacterAttributeEnum)i);
            rate += UnityEngine.Random.Range(0, 100);
            rate /= 100;
            if (rate > 0)
            {
                attributes.Sum((CharacterAttributeEnum)i, rate);
            }
        }
    }

    public int GetIntValue(CharacterStats characterStats)
    {
        return stats.GetIntValue(characterStats);
    }

    public float GetFloatValue(CharacterStats characterStats)
    {
        return stats.GetFloatValue(characterStats);
    }
}
