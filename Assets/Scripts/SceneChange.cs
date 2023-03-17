using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
   public CardController cardController;
   public GameObject maincardController;
   public GameObject player;
   public MainCardController maincard;
   public CardController scenecard;

    private void Start(){
        cardController = FindObjectOfType<CardController>();
        scenecard = FindObjectOfType<CardController>();

    }
   public void LoadScene(string sceneName){

        if(sceneName == "Main"){
            if(cardController.availbleCardSlots[0] && cardController.availbleCardSlots[1] 
                && cardController.availbleCardSlots[2]){
                player.SetActive(true);
                GameManager.Instance.cardScene.SetActive(false);
                GameManager.Instance.mainTIle.SetActive(true);
                maincard.CardSort();
                for(int i=0;i <scenecard.availbleCardSlots.Length; i++){
                scenecard.availbleCardSlots[i] = false;

                }
            }

        }
        else{
            player.SetActive(false);

            GameManager.Instance.cardScene.SetActive(true);
                GameManager.Instance.mainTIle.SetActive(false);
        }
     
   }
}
