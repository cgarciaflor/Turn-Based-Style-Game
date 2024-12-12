using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public class ClearUtility : MonoBehaviour
{
    PathFinding targetPF;
    GridHighlight attackHighlight;
    GridHighlight moveHighlight;

    private void Start()
    {
        StageManager stageManager = FindObjectOfType<StageManager>();
        attackHighlight = stageManager.attackHighlight;
        moveHighlight = stageManager.moveHighlight;
        targetPF = stageManager.pathFinding;
    }

    public void ClearPathfinding()
    {
        targetPF.Clear();
    }

    public void ClearGridHighlightAttack()
    {
        attackHighlight.Hide();
    }

    public void ClearGridHighlightMove()
    {
        moveHighlight.Hide();
    }
}
