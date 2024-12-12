using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    GridManager targetGrid;
    
    PathFinding pathFinding;

    GridHighlight gridHighlight;
    private void Start()
    {
        StageManager stageManager = FindObjectOfType<StageManager>();
        targetGrid = stageManager.stageGrid;
        gridHighlight = stageManager.moveHighlight;
        pathFinding = targetGrid.GetComponent<PathFinding>();
        
        
    }

    public void CheckWalkableTerrain(CharacterScript targetCharacter)
    {
        GridObject gridObj = targetCharacter.GetComponent<GridObject>();
        List<PathNode> walkableNodes = new List<PathNode>();
        pathFinding.Clear();
        pathFinding.CalculateWalkableNodes(gridObj.posOnGrid.x, gridObj.posOnGrid.y, targetCharacter.GetFloatValue(CharacterStats.MovementPoints), ref walkableNodes);
        gridHighlight.Hide();
        gridHighlight.Highlight(walkableNodes);
    }

    public List<PathNode> GetPath(Vector2Int from)
    {

        List<PathNode> path = pathFinding.TraceBackPath(from.x, from.y);
        if (path == null) { return null; }
        if (path.Count == 0) { return null; }
        path.Reverse();


        return path;
        
    }

    public bool CheckOccupied(Vector2Int posOnGrid)
    {
        
        return targetGrid.CheckOccupied(posOnGrid);
    }
}
