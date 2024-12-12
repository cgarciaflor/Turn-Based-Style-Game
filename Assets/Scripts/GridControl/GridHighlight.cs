using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHighlight : MonoBehaviour
{
    GridManager grid;
    [SerializeField] GameObject highlightPoint;
    
    [SerializeField] GameObject container;

    List<GameObject> highlightPointGO;
    void Awake()
    {
        grid = GetComponentInParent<GridManager>();
        highlightPointGO = new List<GameObject>();

        
       
    }

    private GameObject CreatePointHighlightObj()
    {
        GameObject go = Instantiate(highlightPoint);
        highlightPointGO.Add(go);
        go.transform.SetParent(container.transform);
        return go;
    }

    public void Highlight(List<Vector2Int> pos)
    {
        for (int i = 0; i < pos.Count; i++)
        {
            Highlight(pos[i].x, pos[i].y, GetHighlightPointGO(i));
        }
    }

    public void Highlight(List<PathNode> pos)
    {
        for (int i = 0; i < pos.Count; i++)
        {
            Highlight(pos[i].pos_x, pos[i].pos_y, GetHighlightPointGO(i));
        }
    }

    private GameObject GetHighlightPointGO(int i)
    {
        if (highlightPointGO.Count > i)
        {
           return highlightPointGO[i];
        }

        GameObject newHighlight = CreatePointHighlightObj();
        return newHighlight;

    }

    public void Highlight(int posX, int posY, GameObject highlightObj)
    {
        highlightObj.SetActive(true);
        Vector3 pos = grid.GetWorldPOS(posX, posY, true);
        pos += Vector3.up * .2f;
        highlightObj.transform.position = pos;
    }

    public void Hide()
    {
        for (int i = 0;i < highlightPointGO.Count; i++)
        {
            highlightPointGO[i].SetActive(false);
        }
    }
}
