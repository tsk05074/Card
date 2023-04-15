using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    public Grid targetGrid;
    public Vector2Int positionOnGrid;

    void Start()
    {
        targetGrid = GameObject.Find("Grid").GetComponent<Grid>();;
        Init();
    }

    private void Init(){
        positionOnGrid = targetGrid.GetGridPosition(transform.position);
        targetGrid.PlaceObject(positionOnGrid, this);
        Vector3 pos = targetGrid.GetWorldPosition(positionOnGrid.x, positionOnGrid.y, true);
        transform.position = pos;
    }
}
