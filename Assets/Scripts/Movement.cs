using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Movement : MonoBehaviour
{
    [SerializeField]
    private Transform[] movePoint;
    [SerializeField]
    private GameObject playerPrefab;

    public bool isupdown;
    public bool isleftright;
    
    void Start()
    {
        // var player = Instantiate(playerPrefab, new Vector2(movePoint[4].transform.position.x-0.7f,
        // movePoint[4].transform.position.y), movePoint[4].transform.rotation);
        // player.transform.SetParent(movePoint[4].transform);
    }

    public void Vertical(int a){
        if(transform.position.y >= 1 && a==2){
            transform.DOJump(new Vector2(transform.position.x,transform.position.y), 1f, 1, 1f, true);
            transform.Translate(new Vector2(0,0));
        }
        else if(transform.position.y <=-5 && a==-2){
            transform.DOJump(new Vector2(transform.position.x,transform.position.y), 1f, 1, 1f, true);
            transform.Translate(new Vector2(0,0));
        }
        else{
            transform.Translate(new Vector2(0,a));
        }
    }

    public void Horizontal(int a){
         if(transform.position.x <= -7 && a==-4){
            transform.DOJump(new Vector2(transform.position.x,transform.position.y), 1f, 1, 1f, true);
            transform.Translate(new Vector2(0,0));
        }
        else if(transform.position.x >=5 && a==4){
            transform.DOJump(new Vector2(transform.position.x,transform.position.y), 1f, 1, 1f, true);
            transform.Translate(new Vector2(0,0));
        }
        else{
            transform.Translate(new Vector2(a,0));
        }
    }
}
