using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public GameObject Player;

    [SerializeField]
    Transform[] tileTrans;


    void Awake()
    {
        //GameObject player = Instantiate(Player, tileTrans[4].transform.position, tileTrans[4].transform.rotation); 
        Player.transform.parent = tileTrans[4].transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
