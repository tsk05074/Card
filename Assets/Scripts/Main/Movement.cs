using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Movement : MonoBehaviour
{
    public Ease moveEase;

    public void Vertical(int a){

        if(transform.position.y >= 1 && a==2){
            transform.DOJump(new Vector2(transform.position.x,transform.position.y), 1f, 1, 0.5f)
            .SetEase(moveEase);;
        }
        else if(transform.position.y <=-3 && a==-2){
            transform.DOJump(new Vector2(transform.position.x,transform.position.y), 1f, 1, 0.5f)
            .SetEase(moveEase);
        }
        else{
            transform.DOJump(new Vector2(transform.position.x,transform.position.y+a) , 1f, 1, 0.5f)
            .SetEase(moveEase);
        }
    }

    public void Horizontal(int a){

         if(transform.position.x <= -7 && a==-4){
            transform.DOJump(new Vector2(transform.position.x,transform.position.y), 1f, 1, 0.5f)
            .SetEase(moveEase);
        }
        else if(transform.position.x >=5 && a==4){
            transform.DOJump(new Vector2(transform.position.x,transform.position.y), 1f, 1, 0.5f)
            .SetEase(moveEase);           
        }
        else{
            transform.DOJump(new Vector2(transform.position.x + a,transform.position.y), 1f, 1, 0.5f)
            .SetEase(moveEase);          
        }
    }
}
