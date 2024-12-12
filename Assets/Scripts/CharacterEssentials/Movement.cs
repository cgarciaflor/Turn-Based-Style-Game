using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    GridObject gridObject;
    CharacterAnimator characterAnimator;
    List<Vector3> pathWorldPos;
    [SerializeField] float moveSpeed = 1f;
    public bool IS_MOVING {
        get {
            if (pathWorldPos == null) { return false; }
            return pathWorldPos.Count > 0;
        }
    }

    private void Awake()
    {
        gridObject = GetComponent<GridObject>();
        characterAnimator = gameObject.GetComponentInChildren<CharacterAnimator>();
    }
    public void Move(List<PathNode> path)
    {
        if (IS_MOVING)
        {
            SkipAnimation();

        }

        pathWorldPos = gridObject.targetGrid.ConvertPathNodeToWorldPos(path);

        gridObject.targetGrid.RemoveObj(gridObject.posOnGrid, gridObject);

        gridObject.posOnGrid.x = path[path.Count -1].pos_x;
        gridObject.posOnGrid.y = path[path.Count - 1].pos_y;

        gridObject.targetGrid.PlaceObj(gridObject.posOnGrid,gridObject);

        RotateCharacter(transform.position, pathWorldPos[0]);

        characterAnimator.StartMoving();
    }

    public void SkipAnimation()
    {
        if (pathWorldPos.Count < 2) { return; }
        transform.position = pathWorldPos[pathWorldPos.Count - 1];
        Vector3 originPos = pathWorldPos[pathWorldPos.Count - 2];
        Vector3 destPos = pathWorldPos[pathWorldPos.Count - 1];
        RotateCharacter(originPos,destPos);
        pathWorldPos.Clear();
        characterAnimator.StopMoving();
    }

    public void RotateCharacter(Vector3 originPos, Vector3 destinationPos)
    {
        Vector3 direction = (destinationPos - originPos).normalized;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void Update()
    {
        if (pathWorldPos == null) { return; }
        if(pathWorldPos.Count == 0) { return; }

        transform.position = Vector3.MoveTowards(transform.position, pathWorldPos[0], moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, pathWorldPos[0]) < .05f)
        {
            pathWorldPos.RemoveAt(0);
            if (pathWorldPos.Count == 0)
            {
                characterAnimator.StopMoving();
            }
            else
            {
                RotateCharacter(transform.position, pathWorldPos[0]);

            }
        }
    }

}
