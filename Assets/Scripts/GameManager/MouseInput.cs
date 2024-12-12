using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    [SerializeField] Camera mainCam;
    GridManager targetGrid;
    [SerializeField] LayerMask terrainLayerMask;

    public Vector2Int posOnGrid;
    public bool active;

    

    private void Start()
    {
        targetGrid = FindObjectOfType<StageManager>().stageGrid;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue, terrainLayerMask))
        {
            active = true;
            Vector2Int hitPos = targetGrid.GetGridPOS(hit.point);
            if (hitPos != posOnGrid) {
                posOnGrid = hitPos;
                
            }
        }
        else
        {
            active = false;          
        }
    }
}
