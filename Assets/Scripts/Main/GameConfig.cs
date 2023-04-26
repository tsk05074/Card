using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/GameConfig", order = 1)]
public class GameConfig : ScriptableObject
{
     [Header("Overlay Colors")]
        public Color MoveRangeColor;
        public Color AttackRangeColor;
        public Color BlockedTileColor;
}
