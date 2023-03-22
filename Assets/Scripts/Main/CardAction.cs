using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAction : MonoBehaviour
{
    ScriptableCard cardname;

    MouseController mouseController;
    void Start(){
    }   
    public void PerformAction(ScriptableCard _cardname){

        mouseController = FindObjectOfType<MouseController>();

        cardname = _cardname;
        switch(cardname.cardName){
            case "MoveButton" : mouseController.GetInRangeTiles(); break;
            default : Debug.Log("thiere an issue"); break;
        }
    }
}
