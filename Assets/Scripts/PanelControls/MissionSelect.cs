using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionSelect : MonoBehaviour
{
    public void LoadMission(string missionName)
    {
        SceneManager.LoadScene("CombatEssentialScene", LoadSceneMode.Single);
        SceneManager.LoadScene(missionName, LoadSceneMode.Additive);
    }
}
