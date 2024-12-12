using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public CharacterTurn characterTurn;
    public CharacterScript characterScript;
    private CharacterScript target;
    private PathFinding pathfindingScript;
    private GridManager gridManager;
    private List<PathNode> path;
    private int pathIndex = 0;
    private CharacterAnimator characterAnimator;
    private Movement movementScript;

    [SerializeField] private float moveSpeed = 5f; // Constant move speed
    private float movementPoints; // To store movement points for the enemy

    private void Awake()
    {
        characterTurn = GetComponent<CharacterTurn>();
        characterScript = GetComponent<CharacterScript>();
        characterAnimator = GetComponentInChildren<CharacterAnimator>();
        movementScript = GetComponent<Movement>();

        GameObject gridObject = GameObject.Find("Grid");
        if (gridObject != null)
        {
            gridManager = gridObject.GetComponent<GridManager>();
            pathfindingScript = gridObject.GetComponent<PathFinding>();
        }
    }

    private void Update()
    {
        // Only run if it's the enemy's turn
        if (RoundManager.instance.currentTurn == Allegiance.Opponent && characterTurn.canAct)
        {
            StartEnemyTurn();
        }
    }

    private void StartEnemyTurn()
    {
        if (!characterTurn.canAct)
        {
            return; // No actions left for this enemy
        }

        // Get available movement points from character stats
        movementPoints = characterScript.GetFloatValue(CharacterStats.MovementPoints);

        target = DetermineTarget();

        if (target == null)
        {
            Debug.LogWarning("No valid target for enemy AI.");
            EndTurn();
            return;
        }

        if (characterTurn.canWalk)
        {
            Vector3 targetPosition = target.transform.position;
            Vector3 enemyPosition = transform.position;

            Vector2Int startGrid = gridManager.GetGridPOS(enemyPosition);
            Vector2Int endGrid = gridManager.GetGridPOS(targetPosition);

            path = pathfindingScript.FindPath(startGrid.x, startGrid.y, endGrid.x, endGrid.y);

            if (path != null && path.Count > 0)
            {
                pathIndex = 0;
                StartCoroutine(MoveAlongPath());
            }
            else
            {
                Debug.LogWarning("No valid path found for the enemy.");
                EndTurn();
            }
        }

        if (characterTurn.canAct && IsTargetInRange(target))
        {
            AttackTarget(target);
        }

        EndTurn();
    }

    private CharacterScript DetermineTarget()
    {
        List<CharacterScript> playerCharacters = RoundManager.instance.playerForceContainer.GetAllCharacters();
        if (playerCharacters.Count == 0) return null;

        float closestDistance = float.MaxValue;
        CharacterScript closestTarget = null;

        foreach (CharacterScript character in playerCharacters)
        {
            if (character.defeated) continue;

            float distance = Vector3.Distance(transform.position, character.transform.position);
            if (distance < closestDistance)
            {
                closestTarget = character;
                closestDistance = distance;
            }
        }

        return closestTarget;
    }

    private IEnumerator MoveAlongPath()
    {
        List<Vector3> worldPath = gridManager.ConvertPathNodeToWorldPos(path);

        float totalMovementCost = 0f;
        List<Vector3> restrictedPath = new List<Vector3>();

        // Restrict the path based on movement points
        foreach (Vector3 node in worldPath)
        {
            totalMovementCost += 1f; // Assuming each move costs 1 movement point
            if (totalMovementCost > movementPoints)
            {
                break; // Stop if movement points are exceeded
            }
            restrictedPath.Add(node);
        }

        // If there's a valid restricted path, move along it
        if (restrictedPath.Count > 0)
        {
            Vector2Int currentGridPos = gridManager.GetGridPOS(transform.position);
            gridManager.RemoveObj(currentGridPos, GetComponent<GridObject>());

            int nodeIndex = 0; // To track movement through restrictedPath
            while (nodeIndex < restrictedPath.Count)
            {
                Vector3 targetPos = restrictedPath[nodeIndex];
                movementScript.RotateCharacter(transform.position, targetPos);

                // Move towards the target position
                while (Vector3.Distance(transform.position, targetPos) > 0.1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                    yield return null;
                }

                pathIndex++;
                nodeIndex++; // Move to the next node

                if (nodeIndex >= restrictedPath.Count)
                {
                    characterAnimator.StopMoving();
                    if (IsTargetInRange(target)) AttackTarget(target);
                    break;
                }

                characterAnimator.StartMoving();
            }

            // Update the new position on the grid
            Vector2Int newGridPos = gridManager.GetGridPOS(transform.position);
            gridManager.PlaceObj(newGridPos, GetComponent<GridObject>());
        }
        else
        {
            characterAnimator.StopMoving();
            EndTurn(); // End turn if the enemy can't move at all
        }

        // Refresh collider to ensure proper interactions
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
            collider.enabled = true;
        }
    }

    private bool IsTargetInRange(CharacterScript target)
    {
        float attackRange = characterScript.GetIntValue(CharacterStats.AttackRange);
        return Vector3.Distance(transform.position, target.transform.position) <= attackRange;
    }

    private void AttackTarget(CharacterScript target)
    {
        characterAnimator.Attack();
        RotateCharacter(target.transform.position);

        int damage = 250;
        target.TakeDamage(damage);

        Debug.Log($"{characterScript.name} attacked {target.name} for {damage} damage!");
        characterTurn.canAct = false;

        // Check if the player has been defeated after the attack
        DefeatConditionManager defeatManager = FindObjectOfType<DefeatConditionManager>();
        if (defeatManager != null)
        {
            defeatManager.CheckPlayerDefeated();
        }
    }

    private void RotateCharacter(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void EndTurn()
    {
        characterTurn.canAct = false;
        RoundManager.instance.NextTurn(); // Proceed to the next turn
    }
}

