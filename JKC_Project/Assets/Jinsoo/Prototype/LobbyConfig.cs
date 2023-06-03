using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LobbyConfig : MonoBehaviour
{
    private void Awake()
    {
        Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
    }
}
