using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class int2Val
{
    public int current;
    public int max;

    public bool canGoNegative;

    public int2Val(int current, int max)
    {
        this.current = current;
        this.max = max;
    }

    internal void Subtract(int damage)
    {
        current -= damage;

        if (canGoNegative == false) {
            if (current < 0) {
                current = 0;
            }
        }

    }
}

public enum CharacterAttributeEnum
{
    Strength,
    Magic,
    Skill,
    Speed,
    Resistance,
    Defence
}

[Serializable]
public class CharacterAttributes
{
    public const int AttributeCount = 6;

    public int strength;
    public int magic;
    public int skill;
    public int speed;
    public int resistance;
    public int defence;

    public CharacterAttributes()
    {

    }

    public void Sum(CharacterAttributeEnum attribute, int val)
    {
        switch (attribute)
        {
            case CharacterAttributeEnum.Strength:
                strength += val;
                break;
            case CharacterAttributeEnum.Magic:
                magic += val;
                break;
            case CharacterAttributeEnum.Skill:
                skill += val;
                break;
            case CharacterAttributeEnum.Speed:
                speed += val;
                break;
            case CharacterAttributeEnum.Resistance:
                resistance += val;
                break;
            case CharacterAttributeEnum.Defence:
                defence += val;
                break;
        }
    }

    public int Get(CharacterAttributeEnum i)
    {
        switch (i) {
            case CharacterAttributeEnum.Strength:
                return strength;
            case CharacterAttributeEnum.Magic:
                return magic;
            case CharacterAttributeEnum.Skill:
                return skill;
            case CharacterAttributeEnum.Speed:
                return speed;
            case CharacterAttributeEnum.Resistance:
                return resistance;
            case CharacterAttributeEnum.Defence:
                return defence;
        }
        Debug.LogWarning("trying to return att vaule which was not implemented into get method yet");
        return -1;
    }
}

[Serializable]
public class Level
{
    public int RequiredExpToLvlUp
    {
        get
        {
            return level * 1000;
        }
    }

    public int level = 1;
    public int exp = 0;

    public void AddExp(int ex)
    {
        exp += ex;
    }

    public bool CheckLvlUp()
    {
        return exp >= RequiredExpToLvlUp;
    }

    public void LvlUp()
    {
        exp -= RequiredExpToLvlUp;
        level += 1;
    }
}

public enum CharacterStats
{
    HP,
    AttackRange,
    Accuracy,
    Dodge,
    CritChance,
    CritDamageMutli,
    MovementPoints
}

[Serializable]
public class Stats
{
    public float movementPoints = 50f;
    public int hp = 100;

    public int attackRange = 1;
    public float accuracy = .75f;
    public float dodgeChance = .35f;
    public float critChance = 1f;
    public float critMultiplier = 1.5f;

    public float GetFloatValue(CharacterStats stats) {

        switch (stats)
        {
            case CharacterStats.Accuracy:
                return accuracy;
            case CharacterStats.Dodge:
                return dodgeChance;
            case CharacterStats.CritChance:
                return critChance;
            case CharacterStats.CritDamageMutli:
                return critMultiplier;
            case CharacterStats.MovementPoints:
                return movementPoints;
        }

        Debug.Log("incorrect stat type");
        return 0f;
    }

    public int GetIntValue(CharacterStats stats) {
        switch (stats)
        {
            case CharacterStats.HP:
                return hp;
            case CharacterStats.AttackRange:
                return attackRange;
        }

        Debug.Log("incorrect stat type");
        return 0;
    }
}

public class CharacterScript : MonoBehaviour
{
    public CharacterData characterData;
 
    public int2Val hp = new int2Val(100,100);
    public AttackType damageType;
    public bool defeated;

    

    public int GetDefensiveValue(AttackType dt)
    {
        int def = 0;
       
        return def;
    }

    public int GetDamage()
    {
        int damage = 100;
        
        return damage;
    }

    public void TakeDamage(int damage)
    {
        hp.Subtract(damage);
        CheckDefeat();
        Debug.Log("HP: " + hp.current);  
    }

    private void CheckDefeat()
    {
        if (hp.current <= 0) {
            Defeated();
        } else
        {
            Flinch();
        }
       
    }

    CharacterAnimator characterAnimator;

    private void Flinch()
    {
        if (characterAnimator == null) { characterAnimator = GetComponentInChildren<CharacterAnimator>(); }
        characterAnimator.Flinch();
    }

    private void Defeated()
    {
        if (characterAnimator == null) { characterAnimator = GetComponentInChildren<CharacterAnimator>(); }
        defeated = true;
        characterAnimator.Defeated();
    }

    public void AddExp(int exp)
    {
        characterData.AddExp(exp);
    }

    public int GetIntValue(CharacterStats characterStats)
    {
        return characterData.GetIntValue(characterStats);
    }

    public float GetFloatValue(CharacterStats characterStats)
    {
        return characterData.GetFloatValue(characterStats);
    }

    public void SetCharacterData(CharacterData characterData)
    {
        this.characterData = characterData;
    }

    /*public void AddExp(int ex)
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
        for (int i = 0; i < CharacterAttributes.AttributeCount; i++) {
            int rate = lvlUpRates.Get((CharacterAttributeEnum)i);
            rate += UnityEngine.Random.Range(0, 100);
            rate /= 100;
            if (rate > 0)
            {
                attributes.Sum((CharacterAttributeEnum)i, rate);
            }
        }
    }
    */
}
