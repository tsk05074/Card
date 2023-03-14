using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{    
    public Vector2 currenPosition;

    private void Start() {
        currenPosition = transform.position;
    }
   
}
