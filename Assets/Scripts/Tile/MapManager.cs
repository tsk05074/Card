using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using System;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;
    public static MapManager Instance {get {return _instance;}}

    public OverlayTile overlayTilePrefab;
    public OverlayTile characterPosition;

    public GameObject chracterPrefab;

    public GameObject overlayContainer;

    public Dictionary<Vector2Int, OverlayTile> map;

    public CharacterInfo character;
    private MouseController mouseController;
    private BattleController battleController;

    ScriptableCard scriptableCard;

    private void Awake() {
        if(_instance != null && _instance != this){
            Destroy(this.gameObject);
        }
        else{
            _instance = this;
        }
    }
       
    private void Start() {
        mouseController = FindObjectOfType<MouseController>();
        battleController = FindObjectOfType<BattleController>();
        scriptableCard = FindObjectOfType<ScriptableCard>();
        PlayerMap();
    }
    
    public void PlayerMap(){
        var tileMap = gameObject.GetComponentInChildren<Tilemap>();
        var tileMaps = gameObject.transform.GetComponentsInChildren<Tilemap>().OrderByDescending(x => x.GetComponent<TilemapRenderer>().sortingOrder);

        map = new Dictionary<Vector2Int, OverlayTile>();

        BoundsInt bounds = tileMap.cellBounds;

         for(int z = bounds.max.z; z >= bounds.min.z; z--){

            for(int y = bounds.min.y; y < bounds.max.y; y++){

                for(int x = bounds.min.x; x < bounds.max.x; x++){
                    var tileLocation = new Vector3Int(x,y,z);
                    var tileKey = new Vector2Int(x,y);

                    //bool, 해당 위치에 Tile이 있으면 true를 반환함
                    if(tileMap.HasTile(tileLocation)){
                        var overlayTile = Instantiate(overlayTilePrefab, overlayContainer.transform);
                        //Vector3 월드 공간 좌표로 변환된 셀의 중심을 반환함
                        var cellWorldPosition = tileMap.GetCellCenterWorld(tileLocation);

                        overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, cellWorldPosition.z);
                        overlayTile.gameObject.GetComponent<OverlayTile>().gridLocation = new Vector3Int(x, y, z);
                        map.Add(new Vector2Int(x, y), overlayTile.gameObject.GetComponent<OverlayTile>());

                    if(overlayTile.gameObject.GetComponent<OverlayTile>().gridLocation.x == -3 && 
                        overlayTile.gameObject.GetComponent<OverlayTile>().gridLocation.y == 0 && 
                        overlayTile.gameObject.GetComponent<OverlayTile>().gridLocation.z == 0
                    ){
                            
                        characterPosition = overlayTile.GetComponent<OverlayTile>();
                        character = Instantiate(chracterPrefab).GetComponent<CharacterInfo>();
                    }

                    
                }
            }

        }
     }

       mouseController.PositionCharacterOnLine(characterPosition);
        character.standingOnTile = characterPosition;
        //mouseController.GetInRangeTiles();
    }

    public List<OverlayTile> GetSurroundingTiles(Vector2Int originTile, ScriptableCard cardtype)
    {
        var surroundingTiles = new List<OverlayTile>();
        scriptableCard = cardtype;

        switch (scriptableCard.CardType)
        {
            case cardType.Move :
                Vector2Int TileToCheck = new Vector2Int(originTile.x + 1, originTile.y);
                if (map.ContainsKey(TileToCheck))
                {
                    if (Mathf.Abs(map[TileToCheck].transform.position.z - map[originTile].transform.position.z) <= 1)
                        surroundingTiles.Add(map[TileToCheck]);
                }

                TileToCheck = new Vector2Int(originTile.x - 1, originTile.y);
                if (map.ContainsKey(TileToCheck))
                {
                    if (Mathf.Abs(map[TileToCheck].transform.position.z - map[originTile].transform.position.z) <= 1)
                        surroundingTiles.Add(map[TileToCheck]);
                }

                TileToCheck = new Vector2Int(originTile.x, originTile.y + 1);
                if (map.ContainsKey(TileToCheck))
                {
                    if (Mathf.Abs(map[TileToCheck].transform.position.z - map[originTile].transform.position.z) <= 1)
                        surroundingTiles.Add(map[TileToCheck]);
                }

                TileToCheck = new Vector2Int(originTile.x, originTile.y - 1);
                if (map.ContainsKey(TileToCheck))
                {
                    if (Mathf.Abs(map[TileToCheck].transform.position.z - map[originTile].transform.position.z) <= 1)
                        surroundingTiles.Add(map[TileToCheck]);
                }
                return surroundingTiles;

            case cardType.Skill1 :
                Vector2Int TileToCheck2 = new Vector2Int(originTile.x + 1, originTile.y + 1);
                if (map.ContainsKey(TileToCheck2))
                {
                    if (Mathf.Abs(map[TileToCheck2].transform.position.z - map[originTile].transform.position.z) <= 1)
                        surroundingTiles.Add(map[TileToCheck2]);
                }

                TileToCheck2 = new Vector2Int(originTile.x - 1, originTile.y - 1);
                if (map.ContainsKey(TileToCheck2))
                {
                    if (Mathf.Abs(map[TileToCheck2].transform.position.z - map[originTile].transform.position.z) <= 1)
                        surroundingTiles.Add(map[TileToCheck2]);
                }

                TileToCheck2 = new Vector2Int(originTile.x - 1, originTile.y + 1);
                if (map.ContainsKey(TileToCheck2))
                {
                    if (Mathf.Abs(map[TileToCheck2].transform.position.z - map[originTile].transform.position.z) <= 1)
                        surroundingTiles.Add(map[TileToCheck2]);
                }

                TileToCheck2 = new Vector2Int(originTile.x + 1, originTile.y - 1);
                if (map.ContainsKey(TileToCheck2))
                {
                    if (Mathf.Abs(map[TileToCheck2].transform.position.z - map[originTile].transform.position.z) <= 1)
                        surroundingTiles.Add(map[TileToCheck2]);
                }
                return surroundingTiles;

            default : return surroundingTiles;
                

        }

            
        }
    }

    

