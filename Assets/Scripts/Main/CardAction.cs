using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAction : MonoBehaviour
{
    ScriptableCard cardname;
    public Movement moveMent;

    void Start(){
    }   
    public void PerformAction(ScriptableCard _cardname){

        moveMent = GameObject.Find("Player").GetComponent<Movement>();

        cardname = _cardname;
        switch(cardname.cardName){
            case "UpButton" : moveMent.Vertical(2); break;
            case "DownButton" : moveMent.Vertical(-2); break;
            case "LeftButton" : moveMent.Horizontal(-4); break;
            case "RightButton" : moveMent.Horizontal(4); break;
            default : Debug.Log("thiere an issue"); break;
        }
    }
}
