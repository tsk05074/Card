using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public CardController cardController;
    public Card card;

    private void Awake() {
        if(null == instance){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            Destroy(this.gameObject);
        }
    }

    public static GameManager Instance{
        get{
            if(null == instance){
                return null;
            }
            return instance;
        }
    }

    private void Start(){
        cardController = FindObjectOfType<CardController>();
        card = FindObjectOfType<Card>();
    }
}
