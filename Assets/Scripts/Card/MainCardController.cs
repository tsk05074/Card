using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCardController : MonoBehaviour
{
    CardAction cardActions;
    [SerializeField]
    private List<CardDisplay> card = new List<CardDisplay>();
    void Awake()
    {
        cardActions = GetComponent<CardAction>();

        for(int i=0;i<CardController.savedeck.Count; i++){
            card[i].scriptableCard = CardController.savedeck[i];
            Debug.Log(card[i].scriptableCard.cardName);
        }
    }

    void Start(){
        StartCoroutine(CardPlay());
    }

    IEnumerator CardPlay(){
        for(int i=0; i< card.Count;i++){
            cardActions.PerformAction(card[i].scriptableCard);
                    Debug.Log(i);


            yield return new WaitForSeconds(2.0f);
        }
    }


}
