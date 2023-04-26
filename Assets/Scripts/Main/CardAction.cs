using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAction : MonoBehaviour
{
    ScriptableCard cardname;

    MouseController mouseController;
    BattleController battleController;

    void Start(){
    }   
    public void PerformAction(ScriptableCard _cardname){

        mouseController = FindObjectOfType<MouseController>();
        battleController = FindObjectOfType<BattleController>();

        cardname = _cardname;
        switch(cardname.CardType){
            case  cardType.Move : mouseController.GetInRangeTiles(); break;
            case cardType.Skill1 : battleController.GetAttackRangeTiles(); break;
            default : Debug.Log("thiere an issue"); break;
        }
    }
}
