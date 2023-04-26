using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public RangeFinder rangeFinder;
    public List<OverlayTile> rangeAttackTiles;

    public bool isAttack = false;

    private void Start() {
        rangeAttackTiles = new List<OverlayTile>();
    }
    public void GetAttackRangeTiles(){
        isAttack = true;
        rangeAttackTiles = rangeFinder.GetTilesInRange(new Vector2Int(MapManager.Instance.character.standingOnTile.gridLocation.x,
        MapManager.Instance.character.standingOnTile.gridLocation.y),2);

        Debug.Log(rangeAttackTiles);

        foreach(var item in rangeAttackTiles){
            item.SHowTile();
        }
    }
}
