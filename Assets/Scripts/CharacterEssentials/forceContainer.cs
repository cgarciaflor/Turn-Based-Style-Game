using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ForceMember
{
    public CharacterScript character;
    public CharacterTurn CharacterTurn;

    public ForceMember(CharacterScript character, CharacterTurn CharacterTurn)
    {
        this.character = character;
        this.CharacterTurn = CharacterTurn;
    }
}
public class forceContainer : MonoBehaviour
{
    public Allegiance allegiance;
    public List<ForceMember> force;

    public void AddMe(CharacterTurn characterTurn)
    {
        if (force == null) { force = new List<ForceMember>(); }

        force.Add(new ForceMember( characterTurn.GetComponent<CharacterScript>(), characterTurn));
        characterTurn.transform.parent = transform;

    }

    public void GrantTurn()
    {
        for (int i = 0; i < force.Count; i++)
        {
            force[i].CharacterTurn.GrantTurn();
        }
    }

    public bool CheckDefeated()
    {
        for (int i = 0; i < force.Count; i++)
        {
            if (force[i].character.defeated == false)
            {
                return false;
            }
        }
        return true;
    }

    public List<CharacterScript> GetAllCharacters()
    {
        List<CharacterScript> characters = new List<CharacterScript>();
        foreach (ForceMember member in force)
        {
            if (member.character != null)
            {
                characters.Add(member.character);
            }
        }
        return characters;
    }
}
