using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAction : MonoBehaviour
{
    ScriptableCard cardname;

    MouseController mouseController;
    BattleController battleController;

    public void PerformAction(ScriptableCard _cardname){

        mouseController = FindObjectOfType<MouseController>();
        battleController = FindObjectOfType<BattleController>();

        cardname = _cardname;
        switch(cardname.CardType){
            case  cardType.Move : mouseController.GetInRangeTiles(); break;
            case cardType.Skill1 : StartCoroutine(battleController.PlayerAttack(cardname)); break;
            default : Debug.Log("thiere an issue"); break;
        }
    }
}
