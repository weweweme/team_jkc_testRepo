using System;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSizeView : View
{   
    public Button TopButton { get; private set; } 
    public Button MiddleButton { get; private set; } 
    public Button BotButton { get; private set; }

    private void Awake()
    {
        TopButton = transform.Find("TopButton").GetComponent<Button>();
        Debug.Assert(TopButton != null);
        MiddleButton = transform.Find("MiddleButton").GetComponent<Button>();
        Debug.Assert(MiddleButton != null);
        BotButton = transform.Find("BotButton").GetComponent<Button>();
        Debug.Assert(BotButton != null);
    }        

}
