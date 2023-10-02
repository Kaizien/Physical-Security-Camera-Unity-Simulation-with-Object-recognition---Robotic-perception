using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataToCanvas : MonoBehaviour
{
    //Target GameObject
    public Transform target_postion;
    public Camera playerCamera;
    
    
    public TextMeshPro positionText;     // UI Text element on a canvas to display the position

    private void Start()
    {
    }

    void Update()
    {
        
        // Convert world position to screen position
        Vector3 screenPosition = playerCamera.WorldToScreenPoint(target_postion.position);
        
        // Update the UI Text element to show the positionx
        positionText.text = $"Target Position is X: {target_postion.position.x.ToString()}, Y: {target_postion.position.y.ToString()}, Z: {target_postion.position.z.ToString()}\n";
        
        // Place the text element at the screen position
        // Note: This assumes the canvas uses a screen space overlay render mode.
        positionText.transform.position = screenPosition;
    }
    
    

}
