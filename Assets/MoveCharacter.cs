using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    [SerializeField] Grid targetGird;
    Pathfinding pathfinding;

    [SerializeField] GridHighlight gridHiglight;

    private void Start() {
        pathfinding = targetGird.GetComponent<Pathfinding>();
    }

    public void CheckWalkableTerrain(Character targetCharacter)
    {
        GridObject gridObject = targetCharacter.GetComponent<GridObject>();
        List<PathNode> walkableNodes = new List<PathNode>();
        pathfinding.Clear();

        Debug.Log("checkwalkav" + gridObject.positionOnGrid.x + " : " + gridObject.positionOnGrid.y);

        pathfinding.CalculateWalkableNodes(
            gridObject.positionOnGrid.x,
            gridObject.positionOnGrid.y,
            targetCharacter.movementPoints,
            ref walkableNodes
            );

        Debug.Log(walkableNodes);

        gridHiglight.Hide();
        gridHiglight.Highlight(walkableNodes);
    }

    public List<PathNode> GetPath(Vector2Int from){
        List<PathNode> path = pathfinding.TraceBackPath(from.x, from.y);
        path.Reverse();
        //Debug.Log(path.Count);

        if(path == null) {return null;}
        if(path.Count == 0) {return null;}


        return path;
    }
}
