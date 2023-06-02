using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RaiseCountView : View
{
    public Text CountText { get; private set; }
    public Button AddButton { get; private set; }
    public Button ResetButton { get; private set; }

    private void Awake()
    {
        CountText = transform.Find("CountText").GetComponent<Text>();
        AddButton = transform.Find("AddButton").GetComponent<Button>();
        ResetButton = transform.Find("ResetButton").GetComponent<Button>();
    }
}
