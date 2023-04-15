using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    [SerializeField] Grid targetGird;
    [SerializeField] LayerMask terrainLayerMask;

    [SerializeField] GridObject targetCharacter;

    Pathfinding pathfinding;

    List<PathNode> path;

    [SerializeField] GridHighlight gridHighlight;

    private void Start() {
        pathfinding = targetGird.GetComponent<Pathfinding>();
        CheckWalkableTerrain();
    }

    private void CheckWalkableTerrain()
    {
        List<PathNode> walkableNodes = new List<PathNode>();
        pathfinding.CalculatedWalkableNodes(
            targetCharacter.positionOnGrid.x,
            targetCharacter.positionOnGrid.y,
            targetCharacter.GetComponent<Character>().movementPoints,
            ref walkableNodes
        );

        gridHighlight.Highlight(walkableNodes);
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, float.MaxValue, terrainLayerMask)){
                
                
                Vector2Int gridPosition = targetGird.GetGridPosition(hit.point);
                //path = pathfinding.FindPath(targetCharacter.positionOnGrid.x, targetCharacter.positionOnGrid.y, gridPosition.x, gridPosition.y);
                path = pathfinding.TraceBackPath(gridPosition.x, gridPosition.y);
                path.Reverse();

                Debug.Log(path);
                
                if(path == null){return;}
                if(path.Count == 0){return;}
                targetCharacter.GetComponent<Movement>().Move(path);
                
              
            }
        }
    }
}
