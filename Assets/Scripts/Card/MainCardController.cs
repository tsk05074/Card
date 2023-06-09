using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainCardController : MonoBehaviour
{
    CardAction cardActions;
    [SerializeField]
    private List<CardDisplay> card = new List<CardDisplay>();
    private List<cardType> type = new List<cardType>();
    Image image;
    public cardType returnType;
    public CardDisplay cardDiplay;
    public bool moveSelect = false;
    public bool mainCardScene = false;

    void Awake()
    {
        image = GetComponent<Image>();
        cardActions = GetComponent<CardAction>();
        cardDiplay = FindObjectOfType<CardDisplay>();


    }

    public void CardSort(){
         for(int i=0;i<CardController.savedeck.Count; i++){
             card[i].scriptableCard = CardController.savedeck[i];
             card[i].image.sprite = CardController.savedeck[i].cardSprite;
        }

        StartCoroutine(CardPlay());
    }

    public IEnumerator CardPlay(){

        yield return new WaitForSeconds(1.0f);
        for(int i=0; i< CardController.savedeck.Count;i++){
            if(!moveSelect){
                mainCardScene = true;

                cardActions.PerformAction(CardController.savedeck[i]);
                //CardTypeReturn(i);
            }
                yield return new WaitForSeconds(2.0f);

        }
        CardController.savedeck.Clear();

        //GameManager.Instance.cardScene.SetActive(true);
    }

    // public void CardTypeReturn(int _savedeck){
    //     returnType = CardController.savedeck[_savedeck].CardType;
    //     type.Add(returnType);
    // }


}
