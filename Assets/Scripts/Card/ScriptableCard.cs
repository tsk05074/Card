using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class ScriptableCard : ScriptableObject
{
    public cardType CardType;

    [Header("info")]
    public string cardName;
    public Sprite cardSprite;
    public int cardNum;

    [Header("Setting")]
    public int mana;
    public int attackDamage;

}

public enum cardType{
    Move,
    Skill1
}
