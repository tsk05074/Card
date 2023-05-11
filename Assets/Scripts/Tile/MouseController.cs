using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MouseController : MonoBehaviour
{
    public GameObject cursor;

    public GameObject chracterPrefab;

    public float speed;
    public bool isMoving = false;
    private int cardTypeCount = 0;

    public MainCardController mainCard;
    public PathFInder pathFinder;
    public RangeFinder rangeFinder;
    public BattleController battleController;
    private ScriptableCard scriptableCard;

    public List<OverlayTile> path;
    public List<OverlayTile> rangeFInderTiles;

    void start(){
        pathFinder = FindObjectOfType<PathFInder>();
        rangeFinder = FindObjectOfType<RangeFinder>();
        mainCard = FindObjectOfType<MainCardController>();
        battleController = FindObjectOfType<BattleController>();
        scriptableCard = FindObjectOfType<ScriptableCard>();

        path = new List<OverlayTile>();

        rangeFInderTiles = new List<OverlayTile>();
    }

    void LateUpdate()
    {
        RaycastHit2D? hit = GetFocusedOnTile();

        if(hit.HasValue){
            OverlayTile tile = hit.Value.collider.gameObject.GetComponent<OverlayTile>();
            cursor.transform.position = tile.transform.position;
            //CardController.savedeck[cardTypeCount].CardType == cardType.Move
            if(rangeFInderTiles.Contains(tile) && !isMoving && battleController.isAttack == false){
                path = pathFinder.FindPath(MapManager.Instance.character.standingOnTile, tile, rangeFInderTiles);
                for (int i = 0; i < path.Count; i++)
                    {
                        var previousTile = i > 0 ? path[i - 1] : MapManager.Instance.character.standingOnTile;
                        var futureTile = i < path.Count - 1 ? path[i + 1] : null;

                    }
            }

            if(Input.GetMouseButtonDown(0) && mainCard.mainCardScene && battleController.isAttack == false){
                tile.SHowTile();
                if(MapManager.Instance.character == null){
                    //
                }
                else{
                    isMoving = true;
                    //path = pathFinder.FindPath(MapManager.Instance.character.standingOnTile, tile);
                    tile.gameObject.GetComponent<OverlayTile>().HIdeTile();
                }
            }

            
        }

        if(path.Count > 0 && isMoving && battleController.isAttack == false){
            Debug.Log("무빙");
            MoveAlongPath();
        }
    }

    IEnumerator Plus(){
        cardTypeCount++;

        if (cardTypeCount == 3)
        {
            cardTypeCount = 0;
        }
        Debug.Log(cardTypeCount);
        
        yield return null;
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

        if(path.Count == 0){
            isMoving = false;
            mainCard.moveSelect = false;
            mainCard.mainCardScene = false;
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
        MapManager.Instance.character.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y + 0.0001f, tile.transform.position.z);
        MapManager.Instance.character.standingOnTile = tile;
    }

//현재 이동 위치 표시
    public void GetInRangeTiles(){
        mainCard.moveSelect = true;
        //isMoving = true;

        rangeFInderTiles = rangeFinder.GetTilesInRange(new Vector2Int(MapManager.Instance.character.standingOnTile.gridLocation.x,
        MapManager.Instance.character.standingOnTile.gridLocation.y),1);
        foreach(var item in rangeFInderTiles){
            item.SHowTile();
        }
    }
}
