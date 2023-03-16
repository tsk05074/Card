using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCard : MonoBehaviour
{
    [SerializeField]
    private List<CardDisplay> card = new List<CardDisplay>();
    void Awake()
    {
        for(int i=0;i<CardController.savedeck.Count; i++){
            card[i].scriptableCard = CardController.savedeck[i];
        }
    }
}
