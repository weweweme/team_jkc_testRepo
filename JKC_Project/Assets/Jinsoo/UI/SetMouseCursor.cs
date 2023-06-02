using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMouseCursor : MonoBehaviour
{
    private bool isActive;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isActive = !isActive;
            
            if (isActive)
            {
                Cursor.visible = true;
            }
            else
            {
                Cursor.visible = false;
            }
        }
        
        Debug.Log($"PrevIndex : {Model.MapModel.PrevScreenSizeIndex}");
        Debug.Log($"CurrentIndex : {Model.MapModel.CurrentScreenSizeIndex}");
    }
}
