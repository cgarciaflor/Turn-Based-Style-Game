using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryConditionManager : MonoBehaviour
{
    [SerializeField] forceContainer enemyForce;
    [SerializeField] GameObject victoryPanel;
    [SerializeField] MouseInput mouseInput;

    public void CheckPlayerVictory()
    {
        if (enemyForce.CheckDefeated() == true)
        {
            Victory();
        }
    }

    public void Victory()
    {
        mouseInput.enabled = false;
        victoryPanel.SetActive(true);
        Debug.Log("congrats you win");
    }

    public void ReturnToWorldMap()
    {
        SceneManager.LoadScene("TitleScene", LoadSceneMode.Single);
    }
}
