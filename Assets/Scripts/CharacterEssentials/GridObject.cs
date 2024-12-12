using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GridObject : MonoBehaviour
{
    public GridManager targetGrid;

    public Vector2Int posOnGrid;
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        posOnGrid =  targetGrid.GetGridPOS(transform.position);
        targetGrid.PlaceObj(posOnGrid,this);
        Vector3 pos = targetGrid.GetWorldPOS(posOnGrid.x, posOnGrid.y, true);
        transform.position = pos;



    }
}
