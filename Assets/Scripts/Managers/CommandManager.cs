using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CommandType
{
    Moveto,
    Attack
}

public class Command
{
    public CharacterScript character;
    public Vector2Int selectedgrid;
    public CommandType commandType;
    public List<PathNode> path;

    public Command(CharacterScript character, Vector2Int selectedgrid, CommandType commandType)
    {
        this.character = character;
        this.selectedgrid = selectedgrid;
        this.commandType = commandType;
    }

    public GridObject target;
}


public class CommandManager : MonoBehaviour
{
    public Command currCommand;
    CommandInput commandInput;
    VictoryConditionManager victoryConditionManager;
    ClearUtility clearUtility;

    private void Awake()
    {
        victoryConditionManager = GetComponent<VictoryConditionManager>();
        clearUtility = GetComponent<ClearUtility>();
    }

    private void Start()
    {
        commandInput = GetComponent<CommandInput>();

    }

    private void Update()
    {
        if (currCommand != null) {
            ExecuteCommand();
        }
    }

    public void ExecuteCommand()
    {
        switch (currCommand.commandType)
        {
            case CommandType.Moveto:
                MovementCommandExe();
                break;
            case CommandType.Attack:
                AttackCommandExe();
                break;
        }
        
    }

    private void AttackCommandExe()
    {
        CharacterScript receiver = currCommand.character;
        receiver.GetComponent<Attack>().AttackGridObject(currCommand.target);
        receiver.GetComponent<CharacterTurn>().canAct = false;
        victoryConditionManager.CheckPlayerVictory();

        currCommand = null;
        clearUtility.ClearGridHighlightAttack();
    }

    private void MovementCommandExe()
    {
        CharacterScript receiver = currCommand.character;
        receiver.GetComponent<Movement>().Move(currCommand.path);
        receiver.GetComponent<CharacterTurn>().canWalk = false;
        currCommand = null;
        clearUtility.ClearPathfinding();
        clearUtility.ClearGridHighlightMove();
        
    }

    public void AddMoveCommand(CharacterScript character, Vector2Int selectedGrid, List<PathNode> path )
    {
        currCommand = new Command(character, selectedGrid, CommandType.Moveto);
        currCommand.path = path;
    }

    public void AddAttackCommand(CharacterScript attacker, Vector2Int selectGrid, GridObject target)
    {
        currCommand = new Command(attacker, selectGrid, CommandType.Attack);
        currCommand.target = target;
    }
}
