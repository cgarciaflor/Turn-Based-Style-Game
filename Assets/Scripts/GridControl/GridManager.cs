using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class GridManager : MonoBehaviour
{
    Node[,] grid;
    public int width = 25;
    public int length = 25;
    [SerializeField] float cellSize = 1f;
    [SerializeField] LayerMask impossibleLayer;
    [SerializeField] LayerMask terrainLayer;

    public void Awake()
    {
        GenerateGrid();
    }

    public void PlaceObj(Vector2Int posOnGrid, GridObject gridObject)
    {
        grid[posOnGrid.x, posOnGrid.y].gridObj = gridObject;
    }
    
    // x = j
    // y = i


    private void GenerateGrid()
    {
        grid = new Node[length, width];
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < length; j++) {
                grid[j, i] = new Node();
            }
        }
        CalculateElevation();
        CheckPassableTerrain();
    }

    private void CalculateElevation()
    {
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < length; j++) { 
                Ray ray = new Ray(GetWorldPOS(j,i) + Vector3.up * 100f,Vector3.down);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit,float.MaxValue))
                {
                    grid[j,i].elevation = hit.point.y;
                }
            }
        }
    }

    public GridObject GetPlacedObj(Vector2Int gridPOS)
    {
        GridObject gridObject = grid[gridPOS.x, gridPOS.y].gridObj;
        return gridObject;
    }

    private void CheckPassableTerrain()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                Vector3 worldPos = GetWorldPOS(j, i);
                bool passable = !Physics.CheckBox(worldPos, Vector3.one / 2 * cellSize, Quaternion.identity, impossibleLayer);
                grid[j,i].passable = passable;
            }
        }
    }

    public Vector2Int GetGridPOS(Vector3 worldPOS)
    {
        worldPOS.x += cellSize / 2;
        worldPOS.z += cellSize / 2;
        Vector2Int posOnGrid = new Vector2Int((int)(worldPOS.x/cellSize),(int)(worldPOS.z/cellSize));
        return posOnGrid;
    }

    public void OnDrawGizmos()
    {
        if (grid == null) { return; }
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < length; j++)
            {
                Vector3 pos = GetWorldPOS(j, i,true);
                Gizmos.color = grid[j,i].passable ? Color.white : Color.red;
                Gizmos.DrawCube(pos, Vector3.one/4);
            }
        }

    }

    public Vector3 GetWorldPOS(int j, int i, bool elevation = false)
    {
        return new Vector3(j * cellSize, elevation== true ? grid[j,i].elevation : 0f, i * cellSize);
    }

    public bool CheckBoundary(int posX, int posY)
    {
        if (posX < 0 || posX >= length) { return false; }
        if (posY < 0 || posY >= width) {return false; }

        return true;
    }

    public bool CheckWalkable(int pos_x, int pos_y)
    {
        return grid[pos_x, pos_y].passable;
    }

    public List<Vector3> ConvertPathNodeToWorldPos(List<PathNode> path)
    {
        List<Vector3> worldPos = new List<Vector3>();

        for (int i = 0; i < path.Count; i++)
        {
            worldPos.Add(GetWorldPOS(path[i].pos_x, path[i].pos_y, true));

        }

        return worldPos;
    }

    public void RemoveObj(Vector2Int posOnGrid, GridObject gridObject)
    {
        
        grid[posOnGrid.x, posOnGrid.y].gridObj = null;
    }

    public bool CheckOccupied(Vector2Int posOnGrid)
    {
        return GetPlacedObj(posOnGrid) != null;
    }

    public bool CheckElevation(int from_pos_x, int from_pos_y, int to_pos_x, int to_pos_y, float characterClimb = 1.5f)
    {
        float from_elevation = grid[from_pos_x,from_pos_y].elevation;
        float to_elevation = grid[to_pos_x,to_pos_y].elevation;

        float elevation_difference = to_elevation - from_elevation;

        elevation_difference = Mathf.Abs(elevation_difference);

        return characterClimb > elevation_difference;
    }
}
