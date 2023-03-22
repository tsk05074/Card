using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayTile : MonoBehaviour
{
    public int G;
    public int H;

    public int F {get {return G+H;}}

    public OverlayTile previous;

    public Vector3Int gridLocation;

    public bool isBlocked = false;
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            HIdeTile();
        }
    }

     public void SHowTile(){
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255,0,0,1);
    }

    public void HIdeTile(){
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
    }
}
