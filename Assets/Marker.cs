using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    [SerializeField] Transform marker;

    MouseInput mouseInput;

    Vector2Int currentPosition;
    [SerializeField] Grid targetGrid;
    [SerializeField] float elevation = 2f;

    bool active;

    void Awake()
    {
        mouseInput = GetComponent<MouseInput>();
    }

    void Update()
    {
        if(active != mouseInput.active){
            active = mouseInput.active;
            marker.gameObject.SetActive(active);
        }

        if(active == false) { return;}

        if(currentPosition != mouseInput.positionOnGrid){
            currentPosition = mouseInput.positionOnGrid;
            UpdateMarker();
        }
    }

    private void UpdateMarker()
    {
        if (targetGrid.CheckBoundry(currentPosition) == false) { return; }
        Vector3 worldPosition = targetGrid.GetWorldPosition(currentPosition.x, currentPosition.y, true);
        worldPosition.z += elevation;
        marker.position = worldPosition;
    }
}
