using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string Name = "Nameless";
    public float movementPoints;
    public int currentHp = 100;

    public int attackRange = 1;

    public bool TakeDamage(int dmg){
        currentHp -= dmg;

        if(currentHp <= 0){
            return true;
        }
        else{
            return false;
        }
    }
 
}
