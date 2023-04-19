using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    GridObject gridObject;

    List<Vector3> pathWorldPositions;

    [SerializeField] float moveSpeed = 1f;

    public bool IS_MOVING{
        get{
            if(pathWorldPositions == null){return false;}
            return pathWorldPositions.Count > 0;
        }
    }

    private void Awake() {
        gridObject = GetComponent<GridObject>();
    }
    public void Move(List<PathNode> path)
    {
        if(IS_MOVING){
            ForceAnimation();
        }
        pathWorldPositions = gridObject.targetGrid.ConvertPathNodesToWorldPositions(path);

        gridObject.targetGrid.RemoveObject(gridObject.positionOnGrid, gridObject);

         gridObject.positionOnGrid.x = path[path.Count-1].pos_x;
         gridObject.positionOnGrid.y = path[path.Count-1].pos_y;

        gridObject.targetGrid.PlaceObject(gridObject.positionOnGrid, gridObject);

    }

    public void ForceAnimation()
    {
        if(pathWorldPositions.Count < 2) {return;}
        transform.position = pathWorldPositions[pathWorldPositions.Count - 1];
        Vector3 originPosition = pathWorldPositions[pathWorldPositions.Count - 2];
        Vector3 destinationPosition = pathWorldPositions[pathWorldPositions.Count - 1];
        pathWorldPositions.Clear();
    }

    private void Update() {
        if(pathWorldPositions == null) {return;}
        if(pathWorldPositions.Count == 0) {return;}
        
        transform.position = Vector3.MoveTowards(transform.position, pathWorldPositions[0],moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, pathWorldPositions[0]) < 0.05f){
            pathWorldPositions.RemoveAt(0);
        }
    }

}
