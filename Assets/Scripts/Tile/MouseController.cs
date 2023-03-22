using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MouseController : MonoBehaviour
{
    public GameObject cursor;

    public GameObject chracterPrefab;

    public float speed;

    private CharacterInfo character;

    public PathFInder pathFinder;
    public List<OverlayTile> path;

    void start(){
        pathFinder = FindObjectOfType<PathFInder>();
        path = new List<OverlayTile>();
    }

    void LateUpdate()
    {
        RaycastHit2D? hit = GetFocusedOnTile();

        if(hit.HasValue){
            OverlayTile tile = hit.Value.collider.gameObject.GetComponent<OverlayTile>();
            cursor.transform.position = tile.transform.position;

            if(Input.GetMouseButtonDown(0)){
                tile.SHowTile();
                if(MapManager.Instance.character == null){
                    //
                }
                else{
                    path = pathFinder.FindPath(MapManager.Instance.character.standingOnTile, tile);

                    tile.gameObject.GetComponent<OverlayTile>().HIdeTile();
                }
            }
        }

        if(path.Count > 0){
            MoveAlongPath();
        }
    }

    private void MoveAlongPath(){
        var step = speed * Time.deltaTime;

        var zIndex = path[0].transform.position.z;
        MapManager.Instance.character.transform.position = Vector2.MoveTowards(MapManager.Instance.character.transform.position, path[0].transform.position, step);
        MapManager.Instance.character.transform.position = new Vector3(MapManager.Instance.character.transform.position.x, MapManager.Instance.character.transform.position.y, zIndex);

        if(Vector2.Distance(MapManager.Instance.character.transform.position, path[0].transform.position) < 0.00001f){
            PositionCharacterOnLine(path[0]);
            path.RemoveAt(0);
        }
    }

// 마우스 위치 가져오기
    public RaycastHit2D? GetFocusedOnTile(){
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2d = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2d, Vector2.zero);

        if(hits.Length > 0){
            return hits.OrderByDescending(i => i.collider.transform.position.z).First();
        }

        return null;
    }

// 캐릭터 포지션 저장
    public void PositionCharacterOnLine(OverlayTile tile){
        Debug.Log(tile.transform.position);
        MapManager.Instance.character.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y + 0.0001f, tile.transform.position.z);
        MapManager.Instance.character.standingOnTile = tile;
    }
}
