using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode{
    public int pos_x;
    public int pos_y;

    public float  gValue;
    public float  hValue;
    public PathNode parentNode;
    public float fValue{
        get {return gValue + hValue;}
    }

    public PathNode(int xPos, int yPos){
        pos_x = xPos;
        pos_x = yPos;
    }
}
[RequireComponent(typeof(Grid))]
public class Pathfinding : MonoBehaviour
{
    Grid gridMap;
    PathNode[,] pathNodes;
    void Awake()
    {
        Init();
    }

    void Init()
    {

        if(gridMap == null){
            gridMap = GetComponent<Grid>();
        }

        pathNodes = new PathNode[gridMap.length, gridMap.width];
        
        for(int x = 0; x <gridMap.length; x++){
            for(int y = 0; y < gridMap.width; y++){
                pathNodes[x,y] = new PathNode(x,y);
            }
        }
    }

    public void CalculatedWalkableNodes(int startX,int startY, float range, ref List<PathNode> toHightlight){
        PathNode startNode = pathNodes[startX,startY];

        List<PathNode> openList = new List<PathNode>();
        List<PathNode> closedList = new List<PathNode>();

        openList.Add(startNode);

         while(openList.Count > 0){
            PathNode currentNode = openList[0];

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            List<PathNode> neighbourNodes = new List<PathNode>();
            for(int x = -1; x < 2; x++){
                for(int y = -1; y < 2; y++){
                    if(x == 0 && y == 0) {continue;}
                    if(gridMap.CheckBoundry(currentNode.pos_x + x, currentNode.pos_y + y) == false) {continue;}

                    neighbourNodes.Add(pathNodes[currentNode.pos_x + x, currentNode.pos_y + y]);
                }
            }

             for(int i=0; i<neighbourNodes.Count; i++){
                if(closedList.Contains(neighbourNodes[i])) {continue;}
                if(gridMap.CheckWalkable(neighbourNodes[i].pos_x, neighbourNodes[i].pos_y) == false) {continue;}

                float movementCost = currentNode.gValue + CalculateDistance(currentNode, neighbourNodes[i]);

                if(movementCost > range) {continue;}

                if(openList.Contains(neighbourNodes[i]) == false || movementCost < neighbourNodes[i].gValue){
                    
                    neighbourNodes[i].gValue = movementCost;
                    neighbourNodes[i].parentNode = currentNode;

                    if(openList.Contains(neighbourNodes[i]) == false){
                        openList.Add(neighbourNodes[i]);
                    }
                }
            }
         }
            toHightlight.AddRange(closedList);
          
    }

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY){
        PathNode startNode = pathNodes[startX, startY];
        PathNode endNode = pathNodes[endX, endY];
        Debug.Log(endX + " " + endY);

        List<PathNode> openList = new List<PathNode>();
        List<PathNode> closedList = new List<PathNode>();

        openList.Add(startNode);

        while(openList.Count > 0){
            PathNode currentNode = openList[0];
            Debug.Log(openList[0].pos_x + " " + openList[0].pos_y);

            for(int i=0; i< openList.Count; i++){
                if(currentNode.fValue > openList[i].fValue){
                    currentNode = openList[i];
                    Debug.Log(currentNode.pos_x + " : " + currentNode.pos_y);
                }

                if(currentNode.fValue == openList[i].fValue && currentNode.hValue > openList[i].hValue){
                    currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if(currentNode == endNode){
                return RetracePath(startNode, endNode);
            }

            List<PathNode> neighbourNodes = new List<PathNode>();
            for(int x = -1; x < 2; x++){
                for(int y = -1; y < 2; y++){
                    if(x == 0 && y == 0) {continue;}
                    if(gridMap.CheckBoundry(currentNode.pos_x + x, currentNode.pos_y + y) == false) {continue;}

                    neighbourNodes.Add(pathNodes[currentNode.pos_x + x, currentNode.pos_y + y]);
                }
            }

            for(int i=0; i<neighbourNodes.Count; i++){
                if(closedList.Contains(neighbourNodes[i])) {continue;}
                if(gridMap.CheckWalkable(neighbourNodes[i].pos_x, neighbourNodes[i].pos_y) == false) {continue;}

                float movementCost = currentNode.gValue + CalculateDistance(currentNode, neighbourNodes[i]);

                if(openList.Contains(neighbourNodes[i]) == false || movementCost < neighbourNodes[i].gValue){
                    
                    neighbourNodes[i].gValue = movementCost;
                    neighbourNodes[i].hValue = CalculateDistance(neighbourNodes[i], endNode);
                    neighbourNodes[i].parentNode = currentNode;

                    if(openList.Contains(neighbourNodes[i]) == false){
                        openList.Add(neighbourNodes[i]);
                    }
                }
            }
        }


        return new List<PathNode>();;
    }

    private int CalculateDistance(PathNode currentNode, PathNode target)
    {
        int distX = Mathf.Abs(currentNode.pos_x - target.pos_x);
        int distY = Mathf.Abs(currentNode.pos_y - target.pos_y);

        if(distX > distY) {return 14 * distY + 10 * (distX - distY);}
        return 14 * distX + 10 * (distY - distX);
    }

    private List<PathNode> RetracePath(PathNode startNode, PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();

        PathNode currentNode = endNode;

        while(currentNode != startNode){
            path.Add(currentNode);

            currentNode = currentNode.parentNode;
        }
        path.Reverse();
        return path;
    }

    public List<PathNode> TraceBackPath(int x, int y){
        
        if(gridMap.CheckBoundry(x,y) == false) {return null;}
        List<PathNode> path = new List<PathNode>();

        PathNode currentNode = pathNodes[x,y];
        while(currentNode.parentNode != null){
            path.Add(currentNode);
            currentNode = currentNode.parentNode;
        }

        return path;
    }
}
