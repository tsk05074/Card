using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridControl : MonoBehaviour
{
   [SerializeField] Grid targetGrid;
   [SerializeField] LayerMask terrainLayerMask;
   [SerializeField] GridObject hoveringOver;    
   [SerializeField] SelectableGridObject selectedObject;
    Vector2Int currentGridPosition = new Vector2Int(-1,-1);
    private Camera mainCamera;
    private void Start() {
        mainCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    { 
        
        HoverOverObjectCheck();
        SelectObject();
        DeselectObject();
    }

    private void SelectObject(){
          if(Input.GetMouseButtonDown(0)){
            if(hoveringOver == null){return;}
            selectedObject = hoveringOver.GetComponent<SelectableGridObject>();
        }
    }

    private void DeselectObject(){
        if(Input.GetMouseButtonUp(1)){
            selectedObject = null;
        }
    }

    private void HoverOverObjectCheck(){
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, float.MaxValue, terrainLayerMask)){
            
                Vector2Int gridPosition = targetGrid.GetGridPosition(hit.point);

                if(gridPosition == currentGridPosition){return;}
                currentGridPosition = gridPosition;

                GridObject gridObject = targetGrid.GetPlacedObject(gridPosition);
                hoveringOver = gridObject;
            
        }
    }
}
