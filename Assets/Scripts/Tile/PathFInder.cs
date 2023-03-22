using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathFInder : MonoBehaviour
{
     public List<OverlayTile> FindPath(OverlayTile start, OverlayTile end)
        {
            List<OverlayTile> openList = new List<OverlayTile>();
            List<OverlayTile> closedList = new List<OverlayTile>();

            openList.Add(start);

            while (openList.Count > 0)
            {
                OverlayTile currentOverlayTile = openList.OrderBy(x => x.F).First();

                openList.Remove(currentOverlayTile);
                closedList.Add(currentOverlayTile);

                if (currentOverlayTile == end)
                {
                    return GetFinishedList(start, end);
                }

                foreach (var tile in GetNeighbourTiles(currentOverlayTile))
                {
                    if (tile.isBlocked || closedList.Contains(tile) || Mathf.Abs(currentOverlayTile.transform.position.z - tile.transform.position.z) > 1)
                    {
                        continue;
                    }

                    tile.G = GetManhattenDistance(start, tile);
                    tile.H = GetManhattenDistance(end, tile);

                    tile.previous = currentOverlayTile;


                    if (!openList.Contains(tile))
                    {
                        openList.Add(tile);
                    }
                }
            }

            return new List<OverlayTile>();
        }

    private List<OverlayTile> GetFinishedList(OverlayTile start, OverlayTile end){
        List<OverlayTile> finishedList = new List<OverlayTile>();

        OverlayTile currentTile = end;

        while(currentTile != start){
            finishedList.Add(currentTile);
            currentTile = currentTile.previous;
        }
        finishedList.Reverse();

        return finishedList;
    }

    private int GetManhattenDistance(OverlayTile start, OverlayTile neighbour){
        return Mathf.Abs(start.gridLocation.x - neighbour.gridLocation.x) + Mathf.Abs(start.gridLocation.y - neighbour.gridLocation.y);
    }

    private List<OverlayTile> GetNeighbourTiles(OverlayTile currentOverlayTile){
        var map = MapManager.Instance.map;

        List<OverlayTile> neighbours = new List<OverlayTile>();

        //top
        Vector2Int locationToCheck = new Vector2Int(currentOverlayTile.gridLocation.x, currentOverlayTile.gridLocation.y + 1);

        if(map.ContainsKey(locationToCheck)){
            neighbours.Add(map[locationToCheck]);
        }

         //top
        locationToCheck = new Vector2Int(currentOverlayTile.gridLocation.x, currentOverlayTile.gridLocation.y - 1);

        if(map.ContainsKey(locationToCheck)){
            neighbours.Add(map[locationToCheck]);
        }

         //Left
        locationToCheck = new Vector2Int(currentOverlayTile.gridLocation.x -1, currentOverlayTile.gridLocation.y);

        if(map.ContainsKey(locationToCheck)){
            neighbours.Add(map[locationToCheck]);
        }

         //RIght
        locationToCheck = new Vector2Int(currentOverlayTile.gridLocation.x + 1, currentOverlayTile.gridLocation.y);

        if(map.ContainsKey(locationToCheck)){
            neighbours.Add(map[locationToCheck]);
        }

        return neighbours;
    }
}
