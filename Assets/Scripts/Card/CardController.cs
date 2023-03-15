using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public List<Card> deck = new List<Card>();  //카드 현재 덱
    public List<Transform> cardSlots = new List<Transform>();   //넣어지는 카드
    public List<Transform> currentSlots = new List<Transform>();    //다시 돌아가는 카드
    [SerializeField]
    private List<Card> savedeck = new List<Card>();

    //public Transform[] cardSlots;
    public bool[] availbleCardSlots;
    private int Index = 0;

    public void CardSelect(int handIndex){
        Card card = deck[handIndex];

        for(int i=0; i<availbleCardSlots.Length; i++){
            if(!availbleCardSlots[i] && card.transform.position == currentSlots[handIndex].position){
                card.transform.position = cardSlots[i].position;
                availbleCardSlots[i] = true;
                savedeck.Add(card);
                return;
            }
            else if(availbleCardSlots[i] && card.transform.position == cardSlots[i].position){
                card.transform.position = currentSlots[handIndex].position;
                availbleCardSlots[i] = false;
                savedeck.Remove(card);
                return;
            }
        }
      
    }
}

    
