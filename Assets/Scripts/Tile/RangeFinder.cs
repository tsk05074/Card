using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class RangeFinder : MonoBehaviour
{
    private MouseController mouseController;
    private BattleController battleController;

    private MainCardController mainCardController;
    public static int cardTypeCount = 0;
    private void Start() {
        mouseController = FindObjectOfType<MouseController>();
        battleController = FindObjectOfType<BattleController>();
        mainCardController = FindObjectOfType<MainCardController>();
    } 
    public List<OverlayTile> GetTilesInRange(Vector2Int location, int range)
        {
            var startingTile = MapManager.Instance.map[location];
            var inRangeTiles = new List<OverlayTile>();
            int stepCount = 0;

            inRangeTiles.Add(startingTile);

            //Should contain the surroundingTiles of the previous step. 
            var tilesForPreviousStep = new List<OverlayTile>();
            tilesForPreviousStep.Add(startingTile);
            while (stepCount < range)
            {
                

                var surroundingTiles = new List<OverlayTile>();

                foreach (var item in tilesForPreviousStep)
                {
                    surroundingTiles.AddRange(MapManager.Instance.GetSurroundingTiles(new Vector2Int(item.gridLocation.x, item.gridLocation.y), CardController.savedeck[cardTypeCount]));
                }

                inRangeTiles.AddRange(surroundingTiles);
                tilesForPreviousStep = surroundingTiles.Distinct().ToList();
                stepCount++;
                cardTypeCount++;

                if(cardTypeCount==3){
                    cardTypeCount=0;
                }
                Debug.Log(cardTypeCount);
            }

            return inRangeTiles.Distinct().ToList();
        }
}
