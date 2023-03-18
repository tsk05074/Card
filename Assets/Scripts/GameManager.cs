using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public GameObject mainScene;
    public GameObject mainTIle;
    public GameObject cardScene;

    public MainCardController mainCard;
    public SceneChange sceneChange;
    public CardDisplay cardDisplay;
    private void Awake() {
        if(null == instance){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            Destroy(this.gameObject);
        }
    }

    void Start(){
        mainCard = FindObjectOfType<MainCardController>();
        sceneChange = FindObjectOfType<SceneChange>();
        cardDisplay = FindObjectOfType<CardDisplay>();
    }

    public static GameManager Instance{
        get{
            if(null == instance){
                return null;
            }
            return instance;
        }
    }
}
