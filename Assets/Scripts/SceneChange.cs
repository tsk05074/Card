using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
   public CardController cardController;
   public GameObject maincardController;
   public MainCardController maincard;
   public CardController scenecard;

   public Camera myCamera;
   public Camera minimapCamera;

    private void Start(){
        cardController = FindObjectOfType<CardController>();
        scenecard = FindObjectOfType<CardController>();

    }
   public void LoadScene(string sceneName){

        if(sceneName == "Main"){
            if(cardController.availbleCardSlots[0] && cardController.availbleCardSlots[1] 
                && cardController.availbleCardSlots[2]){
                GameManager.Instance.cardScene.SetActive(false);
                maincard.CardSort();
                for(int i=0;i <scenecard.availbleCardSlots.Length; i++){
                scenecard.availbleCardSlots[i] = false;
                minimapCamera.gameObject.SetActive(false);
                MainCameraLayer();
                }
            }

        }
        else{
            GameManager.Instance.cardScene.SetActive(true);
            minimapCamera.gameObject.SetActive(true);
            CardCamera();
        }
   }

   public void MainCameraLayer(){
        myCamera.cullingMask = ~(1 << 8);
   }

   public void CardCamera(){
        myCamera.cullingMask = 1 << 5;
   }
}
