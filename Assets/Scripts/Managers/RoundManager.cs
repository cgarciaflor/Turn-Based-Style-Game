using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance;


    private void Awake()
    {
        instance = this;
    }


    [SerializeField] public forceContainer playerForceContainer;
    [SerializeField] forceContainer opponentForceContainer;

    int round = 1;

    [SerializeField] TMPro.TextMeshProUGUI turnCountText;
    [SerializeField] TMPro.TextMeshProUGUI forceRoundText;

    [SerializeField] MouseInput mouseInput;
    private void Start()
    {
        UpdateTextOnScreen();
    }

    public void AddMe(CharacterTurn character)
    {
 
        if (character.allegiance == Allegiance.Player)
        {
            playerForceContainer.AddMe(character);
        }

        if (character.allegiance == Allegiance.Opponent) { 
            opponentForceContainer.AddMe(character);
        }
        
    }

    public Allegiance currentTurn;
    public void NextTurn()
    {
        switch (currentTurn) {
            case Allegiance.Player:
                DisablePlayerInput();
                currentTurn = Allegiance.Opponent;
                break;
            case Allegiance.Opponent:
                NextRound();
                EnablePlayerInput();
                currentTurn = Allegiance.Player;
                break;
        }

        GrantTurnToForce();
        UpdateTextOnScreen();
    }

    public void EnablePlayerInput()
    {
        mouseInput.enabled = true;
    }

    public void DisablePlayerInput()
    {
        mouseInput.enabled = false;
    }

    public void NextRound()
    {
        round += 1;

  
    }

    public void GrantTurnToForce()
    {
        switch (currentTurn) {
            case Allegiance.Player:
                playerForceContainer.GrantTurn();
                break;
            case Allegiance.Opponent:
                opponentForceContainer.GrantTurn();
                break;
        }
    }

    void UpdateTextOnScreen()
    {
        turnCountText.text = "Turn: " + round.ToString();
        forceRoundText.text = currentTurn.ToString();
    }
}
