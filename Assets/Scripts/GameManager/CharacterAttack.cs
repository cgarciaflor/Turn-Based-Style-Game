using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    
    [SerializeField] GridManager targetGrid;
    [SerializeField] GridHighlight highlight;
    

    List<Vector2Int> attackPos;

    private void Start()
    {
        StageManager stageManager = FindObjectOfType<StageManager>();
        targetGrid = stageManager.stageGrid;
        highlight = stageManager.attackHighlight;
    }

    /*    private void Start()
        {
            CalculateAttackArea();  
        }
        public void CalculateAttackArea(bool selfTargetable = false)
        {
            CharacterScript character = selectedChar.GetComponent<CharacterScript>();
            int attackRange = character.attackRange;

            attackPos = new List<Vector2Int>();

            for (int x =- attackRange; x <= attackRange; x++) {
                for (int y = -attackRange; y <= attackRange; y++) { 
                    if (Mathf.Abs(x) +  Mathf.Abs(y) > attackRange) { continue; }
                    if (selfTargetable == false)
                    {
                        if (x == 0 && y == 0) { continue; }
                    }
                    if(targetGrid.CheckBoundary(selectedChar.posOnGrid.x+x, selectedChar.posOnGrid.y + y) == true)
                    {
                        attackPos.Add(new Vector2Int(selectedChar.posOnGrid.x + x,selectedChar.posOnGrid.y + y));


                    }
                }
            }
            highlight.Highlight(attackPos);
        }
    */
    public void CalculateAttackArea(Vector2Int characterPosOnGrid, int attackRange,bool selfTargetable = false)
    {
        if (attackPos == null) { 
            attackPos = new List<Vector2Int>();
        } else
        {
            attackPos.Clear();
        }

        for (int x = -attackRange; x <= attackRange; x++)
        {
            for (int y = -attackRange; y <= attackRange; y++)
            {
                if (Mathf.Abs(x) + Mathf.Abs(y) > attackRange) { continue; }
                if (selfTargetable == false)
                {
                    if (x == 0 && y == 0) { continue; }
                }
                if (targetGrid.CheckBoundary(characterPosOnGrid.x + x, characterPosOnGrid.y + y) == true)
                {
                    attackPos.Add(new Vector2Int(characterPosOnGrid.x + x, characterPosOnGrid.y + y));


                }
            }
        }
        highlight.Highlight(attackPos);
    }

    public bool Check(Vector2Int posOnGrid)
    {
        return attackPos.Contains(posOnGrid);
    }

    public GridObject GetAttackTarget(Vector2Int posOnGrid)
    {
        GridObject target = targetGrid.GetPlacedObj(posOnGrid);
        return target;
    }
}
