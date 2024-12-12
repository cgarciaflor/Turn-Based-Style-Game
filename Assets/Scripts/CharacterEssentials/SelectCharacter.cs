using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    MouseInput mouseInput;
    CommandMenu commandMenu;
    GameMenu gameMenu;

    private void Awake()
    {
        mouseInput = GetComponent<MouseInput>();
        commandMenu = GetComponent<CommandMenu>();
        gameMenu = GetComponent<GameMenu>();
    }

    public CharacterScript select;
    bool isSelected;
    Vector2Int posOnGrid = new Vector2Int(-1,-1);
    GridManager targetGrid;
    GridObject hoverOverGridObj;
    public CharacterScript hoverOverChar;

    private void Start()
    {
        targetGrid = FindObjectOfType<StageManager>().stageGrid;
    }

    public void Update()
    {
        if(posOnGrid != mouseInput.posOnGrid)
        {
            HoverOverObject();
        }
        SelectInput();
        DeselectInput();
    }

    private void LateUpdate()
    {
        if (select != null) {
            if (isSelected == false) { 
                select = null;
            }
        }
    }

    private void HoverOverObject()
    {
        
            posOnGrid = mouseInput.posOnGrid;
            hoverOverGridObj = targetGrid.GetPlacedObj(posOnGrid);
            if (hoverOverGridObj != null)
            {
                hoverOverChar = hoverOverGridObj.GetComponent<CharacterScript>();
            }
            else
            {
                hoverOverChar = null;
            }
        
    }

    private void DeselectInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            select = null;
            UpdatePanel();
        }
    }

    private void UpdatePanel()
    {
        if (select != null) {
            commandMenu.OpenPanel(select.GetComponent<CharacterTurn>());
        }
        else
        {
            commandMenu.ClosePanel();
        }
    }

    private void SelectInput()
    {
        HoverOverObject();
        if (select != null) { return; }
        if (gameMenu.panel.activeInHierarchy == true) { return; }
        if (Input.GetMouseButtonDown(0))
        {
            if (hoverOverChar != null && select == null)
            {
                select = hoverOverChar;
                isSelected = true;
            }
            UpdatePanel();
        }
    }

    public void Deselect()
    {
        isSelected = false;
    }
}
