using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    Node[,] grid;
     public int width = 5;
     public int length = 6;
    [SerializeField] float cellSize = 1f;
    [SerializeField] LayerMask obstacleLayer;
    [SerializeField] LayerMask terrinLayer;

    [SerializeField] GameObject Tiles;
    [SerializeField] GameObject Tiles_parent;

    private void Awake() {


        GeneraterGrid();
    }

    private void GeneraterGrid(){
        grid = new Node[length, width];

        for(int y = 0; y < width; y++){
            for(int x = 0; x < length; x++){
                Vector3 pos = GetWorldPosition(x,y);
                 var obj = Instantiate(Tiles);
                obj.transform.SetParent(Tiles_parent.transform);
                obj.transform.localPosition = new Vector3(pos.x, pos.y, 0);
                grid[x,y] = new Node();
            }
        }


        if(grid == null){
            Debug.Log("null");
        }

        CheckPassableTerrain();
        CalculateElevation();
    }

    public void PlaceObject(Vector2Int positionOnGrid, GridObject gridObject)
    {
        if(CheckBoundry(positionOnGrid) == true){
            grid[positionOnGrid.x, positionOnGrid.y].gridObject = gridObject;
        }

    }

    public GridObject GetPlacedObject(Vector2Int gridPosition){
        GridObject gridObject = grid[gridPosition.x, gridPosition.y].gridObject;
        return gridObject;
    }

    public bool CheckBoundry(Vector2Int positionOnGrid){
        if(positionOnGrid.x < 0 || positionOnGrid.x >= width){
            return false;
        }
        if(positionOnGrid.y < 0 || positionOnGrid.y >= length){
            return false;
        }

        return true;
    }

        internal bool CheckBoundry(int posX, int poxY)
    {
        if(posX < 0 || posX >= width){
            return false;
        }
        if(poxY < 0 || poxY >= length){
            return false;
        }

        return true;
    }

    private void CheckPassableTerrain(){
        for(int y = 0; y < width; y++){
            for(int x = 0; x < length; x++){
                Vector3 worldPosition = GetWorldPosition(x,y);
                bool passable = !Physics.CheckBox(worldPosition, Vector3.one/2 * cellSize, Quaternion.identity, obstacleLayer);
                grid[x,y] = new Node();
                grid[x,y].passable = passable;
            }
        }
    }

    private void CalculateElevation(){
        for(int y = 0; y < width; y++){
            for(int x = 0; x < length; x++){
                Ray ray = new Ray(GetWorldPosition(x,y), Vector3.down);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit, float.MaxValue, terrinLayer)){
                    grid[x,y].elevation = hit.point.z;
                }
               
            }
        }
    }

    public bool CheckWalkable(int pos_x, int pos_y)
    {
        return grid[pos_x, pos_y].passable;
    }

    public Vector2Int GetGridPosition(Vector3 worldPosition){
        worldPosition.x += cellSize / 2;
        worldPosition.y += cellSize / 2;
        Vector2Int positionOnGrid = new Vector2Int((int)(worldPosition.x / cellSize), (int)(worldPosition.y / cellSize));
        return positionOnGrid;
    }

    private void OnDrawGizmos() {
        for(int y = 0; y < width; y++){
            for(int x = 0; x < length; x++){
                Vector3 pos = GetWorldPosition(x,y);
                Gizmos.DrawCube(pos, Vector3.one/4);
            }
        }
    }

    public Vector3 GetWorldPosition(int x, int y, bool elevation = false){
        return new Vector3(transform.position.x + (x * cellSize), transform.position.y + (y * cellSize), elevation == true? grid[x,y].elevation : 0f);
        //return new Vector3(x * cellSize - 2.5f, y*cellSize -1.5f, elevation == true? grid[x,y].elevation : 0f);
    }

    public List<Vector3> ConvertPathNodesToWorldPositions(List<PathNode> path)
    {
        List<Vector3> worldPositions = new List<Vector3>();

        for(int i=0; i<path.Count; i++){
            worldPositions.Add(GetWorldPosition(path[i].pos_x, path[i].pos_y, true));
        }
        return worldPositions;
    }
}
