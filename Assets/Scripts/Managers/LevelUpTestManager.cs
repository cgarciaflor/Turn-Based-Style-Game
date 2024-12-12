using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpTestManager : MonoBehaviour
{
    public CharacterScript targetChar; 

    public void AddEXP(int exp)
    {
        targetChar.AddExp(exp);
    }

}
