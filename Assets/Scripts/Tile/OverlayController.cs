using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayController : MonoBehaviour
{
    private static OverlayController _instance;
    public static OverlayController Instance{get {return _instance;}}
    public Dictionary<Color, List<OverlayTile>> coloredTiles;
    //public GameConfig gameConfig;

    public Color AttackRangeColor;
    public Color MoveRangeColor;

    public enum TileColors{
        MovementColor,
        AttackRangeColor
    }
    private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }

            coloredTiles = new Dictionary<Color, List<OverlayTile>>();
            //MoveRangeColor = gameConfig.MoveRangeColor;
            //AttackRangeColor = gameConfig.AttackRangeColor;
            //BlockedTileColor = gameConfig.BlockedTileColor;
        }


}
