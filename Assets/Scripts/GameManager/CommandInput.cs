using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInput : MonoBehaviour
{
    CommandManager commandManager;
    MouseInput mouseInput;
    MoveCharacter moveCharacter;
    CharacterAttack characterAttack;
    SelectCharacter selectCharacter;
    ClearUtility clearUtility;

    private void Awake()
    {
        commandManager = GetComponent<CommandManager>();
        mouseInput = GetComponent<MouseInput>();
        moveCharacter = GetComponent<MoveCharacter>();
        characterAttack = GetComponent<CharacterAttack>();
        selectCharacter = GetComponent<SelectCharacter>();
        clearUtility = GetComponent<ClearUtility>();
    }

    [SerializeField] CommandType currCommand;
    bool isInputCommand;

    public void SetCommandType(CommandType commandType)
    {
        currCommand = commandType;
    }

    public void InitCommand()
    {
        isInputCommand = true;
        switch (currCommand) { 
            case CommandType.Moveto:
                HighlightWalkableTerrain();
                break;
            case CommandType.Attack:
                characterAttack.CalculateAttackArea(selectCharacter.select.GetComponent<GridObject>().posOnGrid, selectCharacter.select.GetIntValue(CharacterStats.AttackRange));
                break;
        }
    }

    public void HighlightWalkableTerrain()
    {
        moveCharacter.CheckWalkableTerrain(selectCharacter.select);
    }

    private void Update()
    {
        if (isInputCommand == false) { return; }
        switch (currCommand) {
            case CommandType.Moveto:
                MoveCommandInput();
                break;
            case CommandType.Attack:
                AttackCommandInput();
                break;
        }
    }

    private void AttackCommandInput()
    {
        if (Input.GetMouseButtonDown(0)) { 
            if(characterAttack.Check(mouseInput.posOnGrid) == true)
            {
                GridObject gridObj = characterAttack.GetAttackTarget(mouseInput.posOnGrid);
                if (gridObj == null) { return; }
                commandManager.AddAttackCommand(selectCharacter.select, mouseInput.posOnGrid, gridObj);
                StopCommandInput();
            }

        }
        if (Input.GetMouseButtonDown(1))
        {
            StopCommandInput();
            clearUtility.ClearGridHighlightAttack();
        }
    }

    private void MoveCommandInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (moveCharacter.CheckOccupied(mouseInput.posOnGrid) == true) { return; }
            List<PathNode> path = moveCharacter.GetPath(mouseInput.posOnGrid);
            if (path == null) { return; }
            if (path.Count == 0) { return; }
            commandManager.AddMoveCommand(selectCharacter.select, mouseInput.posOnGrid, path);
            StopCommandInput();
        }
        if (Input.GetMouseButtonDown(1))
        {
            StopCommandInput();
            clearUtility.ClearGridHighlightMove();
            clearUtility.ClearPathfinding();
        }
    }

    private void StopCommandInput()
    {
        selectCharacter.Deselect();
        selectCharacter.enabled = true;
        isInputCommand = false;
    }
}
