using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField]Grid targetGird;
    [SerializeField] LayerMask terrainLayerMask;

    public Vector2Int positionOnGrid;

    public bool active;

    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            active = true;
            if(Physics.Raycast(ray, out hit, float.MaxValue, terrainLayerMask)){
                
                
                Vector2Int hitPosition = targetGird.GetGridPosition(hit.point);
               
                if(hitPosition != positionOnGrid){
                    positionOnGrid = hitPosition;
                }
            }
            else{
                active = false;
            }
        }
    }
}
