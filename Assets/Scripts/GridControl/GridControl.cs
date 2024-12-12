using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridControl : MonoBehaviour
{
    [SerializeField] GridManager targetGird;
    [SerializeField] LayerMask terrainLayerMask;
    [SerializeField] GridObject hoveringOver;
    [SerializeField] SelectableGridObject selectedObj;
    Vector2Int currentGridPos = new Vector2Int(-1,-1);

    private void Update()
    {
        HoverOverObjCheck();

        SelectObj();

        DeselectObj();
    }

    private void DeselectObj()
    {
        if (Input.GetMouseButtonDown(1))
        {
            selectedObj = null;
        }
    }

    private void SelectObj()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (hoveringOver == null) { return; }
            selectedObj = hoveringOver.GetComponent<SelectableGridObject>();
        }
    }

    private void HoverOverObjCheck()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue, terrainLayerMask))
        {
            Vector2Int gridPOS = targetGird.GetGridPOS(hit.point);
            if (gridPOS == currentGridPos) { return; }
            currentGridPos = gridPOS;
            GridObject gridObject = targetGird.GetPlacedObj(gridPOS);
            hoveringOver = gridObject;
        }
    }
}
