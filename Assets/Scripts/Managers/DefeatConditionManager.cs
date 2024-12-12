using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatConditionManager : MonoBehaviour
{
    [SerializeField] forceContainer playerForce;
    [SerializeField] GameObject defeatPanel;
    [SerializeField] MouseInput mouseInput;

    public void CheckPlayerDefeated()
    {
        if (playerForce.CheckDefeated() == true)
        {
            Defeat();
        }
    }

    public void Defeat()
    {
        mouseInput.enabled = false;
        defeatPanel.SetActive(true);
        Debug.Log("You Lose");
    }

    public void ReturnToHomeScreen()
    {
        SceneManager.LoadScene("TitleScene", LoadSceneMode.Single);
    }
}
