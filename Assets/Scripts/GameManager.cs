using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public CardController cardController;
    public CardDisplay cardDisplay;
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
        cardDisplay = FindObjectOfType<CardDisplay>();
    }

    public void MainScene(){
        if(cardController.availbleCardSlots[0] && cardController.availbleCardSlots[1] 
        && cardController.availbleCardSlots[2]){
            SceneManager.LoadScene("Main");
        }
    }
}
