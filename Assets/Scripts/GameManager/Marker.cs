using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    MouseInput mouseInput;
    bool active;
    [SerializeField] Transform marker;
    GridManager targetGrid;
    [SerializeField] float elevation = 2f;

    Vector2Int currPos;

    private void Awake()
    {
        mouseInput = GetComponent<MouseInput>();
    }
    private void Start()
    {
        targetGrid = FindObjectOfType<StageManager>().stageGrid;
    }

    private void Update()
    {
        if (active != mouseInput.active)
        {
            active = mouseInput.active;
            marker.gameObject.SetActive(active);
        }
        if (active == false) { return; }
        if (currPos != mouseInput.posOnGrid) { 
            currPos = mouseInput.posOnGrid;
            UpdateMarker();
        }
    }

    private void UpdateMarker()
    {

        Vector3 worldPos = targetGrid.GetWorldPOS(currPos.x, currPos.y, true);
        worldPos.y += elevation;
        marker.position = worldPos;
    }
}
