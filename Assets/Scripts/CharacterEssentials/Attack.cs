using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public enum AttackType
{
    Physical,
    Magical
}

public class Attack : MonoBehaviour
{
    GridObject gridObject;
    CharacterAnimator characterAnimator;
    CharacterScript character;

    private void Awake()
    {
        character = GetComponent<CharacterScript>();
        gridObject = GetComponent<GridObject>();
        characterAnimator = GetComponentInChildren<CharacterAnimator>();
    }

    public void AttackGridObject(GridObject targetGridObj)
    {
        RotateCharacter(targetGridObj.transform.position);
        characterAnimator.Attack();

        // accuracy ht check
        if (UnityEngine.Random.value >= character.GetFloatValue(CharacterStats.Accuracy)) { 
            Debug.Log("Miss"); 
            return; 
        }
        CharacterScript target = targetGridObj.GetComponent<CharacterScript>();

        // check if attack is dodged
        if (UnityEngine.Random.value <= target.GetFloatValue(CharacterStats.Dodge))
        {
            Debug.Log("Dodge");
            return;
        }
        
        int damage = character.GetDamage();

        if (UnityEngine.Random.value <= character.GetFloatValue(CharacterStats.CritChance))
        {
            damage = (int)(damage * character.GetFloatValue(CharacterStats.CritDamageMutli));
            Debug.Log("Crit");
        }
        damage -= target.GetDefensiveValue(character.damageType);

    
        if (damage <= 0)
        {
            damage = 1;
        }
        Debug.Log("target took: " + damage.ToString());


        target.TakeDamage(damage);
    }

    public void RotateCharacter(Vector3 towards)
    {
        Vector3 direction = (towards - transform.position).normalized;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
