using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public List<Transform> cardSlots = new List<Transform>();
    public List<Transform> currentSlots = new List<Transform>();

    //public Transform[] cardSlots;
    public bool[] availbleCardSlots;
    public bool hasBeenPlayer;
    private int Index = 0;

    public void Start(){
        for(int i=0; i<availbleCardSlots.Length; i++){
            if(availbleCardSlots[i] == true){
                //card.transform.position = cardSlots[i].position;
                availbleCardSlots[i] = false;
            }
        }
    }

    public void CardSelect(int handIndex){

        if(!availbleCardSlots[handIndex] ){
            deck[handIndex].transform.position = cardSlots[handIndex].position;
            availbleCardSlots[handIndex] = true;
        }
        else if(availbleCardSlots[handIndex]){
            deck[handIndex].transform.position = currentSlots[handIndex].transform.position;
            availbleCardSlots[handIndex] = false;

        }
    }
}

    
