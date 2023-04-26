using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardDisplay : MonoBehaviour
{
    public ScriptableCard scriptableCard;
    public Text text;
    public Image image;
    void Start()
    {
        //text.text = scriptableCard.cardName;
        image.sprite = scriptableCard.cardSprite;
    }
}
