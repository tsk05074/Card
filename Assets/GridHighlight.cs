using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHighlight : MonoBehaviour
{
    Grid grid;
    [SerializeField] GameObject hilightPoint;
    List<GameObject> highlightPointGO;
    [SerializeField] GameObject container;
    void Awake()
    {
        grid = GetComponentInParent<Grid>();
        highlightPointGO = new List<GameObject>();
        //Highlight(testTargetPosition);
    }

    private GameObject CreateMovePointHighlightObject(){
        GameObject go = Instantiate(hilightPoint);
        highlightPointGO.Add(go);
        go.transform.SetParent(container.transform);
        return go;
    }

    public void Highlight(List<Vector2Int> positions){
        for(int i=0; i< positions.Count; i++){
            Highlight(positions[i].x, positions[i].y, GetHighlightPointGo(i));
        }
    }

    public void Highlight(List<PathNode> positions){
        for(int i=0; i< positions.Count; i++){
            Highlight(positions[i].pos_x, positions[i].pos_y, GetHighlightPointGo(i));
        }
    }

    private GameObject GetHighlightPointGo(int i)
    {
        if(highlightPointGO.Count < i){
            return highlightPointGO[i];
        }

        GameObject newHighlightObject = CreateMovePointHighlightObject();
        return newHighlightObject;
    }

    public void Highlight(int posX, int posY, GameObject highlightObject)
    {
       Vector3 position = grid.GetWorldPosition(posX, posY, true);
       position += Vector3.forward * 0.2f;
       highlightObject.transform.position = position;
    }
}
