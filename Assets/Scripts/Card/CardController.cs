using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    [SerializeField]
    private List<Card> deck = new List<Card>();  //카드 현재 덱
    [SerializeField]
    private List<Transform> cardSlots = new List<Transform>();   //카드 슬롯 덱
    [SerializeField]
    private List<Transform> currentSlots = new List<Transform>();    //카드 돌아가는 덱
    public static List<CardDisplay> savedeck = new List<CardDisplay>(); //선택된 카드 저장 덱
    public bool[] availbleCardSlots;    //슬록이 비었는지 체크

    void Start(){
        // for(int i=0; i<savedeck.Count;i++){
        //     savedeck[i] = null;
        // }
    }
    public void CardSelect(int handIndex){
        Card card = deck[handIndex];
        CardDisplay objCard = new CardDisplay();
        objCard.scriptableCard = card.cardSciprtable;
        //objCard.text = card.text;
        // objCard.scriptableCard = 

        for(int i=0; i<availbleCardSlots.Length; i++){
            if(!availbleCardSlots[i] && card.transform.position == currentSlots[handIndex].position){
                card.transform.position = cardSlots[i].position;
                availbleCardSlots[i] = true;
                savedeck.Add(objCard);
                return;
            }
            else if(availbleCardSlots[i] && card.transform.position == cardSlots[i].position){
                card.transform.position = currentSlots[handIndex].position;
                availbleCardSlots[i] = false;
                savedeck.Remove(objCard);
                return;
            }
        }
      
    }
}

    
