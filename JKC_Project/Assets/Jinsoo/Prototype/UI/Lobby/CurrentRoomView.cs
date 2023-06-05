using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;
using UnityEngine.UI;

public class CurrentRoomView : View
{
    public Text CurrentPlayerCount { get; private set; }
    public Text AnnouncementText { get; private set; }
    public Text StartGameCount { get; private set; }
    public Text NotInRoomText { get; private set; }

    private CurrentRoomPresenter _currentRoomPresenter = new CurrentRoomPresenter(); 

    private void Awake()
    {
        CurrentPlayerCount = transform.Find("CurrentPlayerCount").GetComponent<Text>();
        Debug.Assert(CurrentPlayerCount != null);
        AnnouncementText = transform.Find("AnnouncementText").GetComponent<Text>();
        Debug.Assert(AnnouncementText != null);
        StartGameCount = transform.Find("StartGameCount").GetComponent<Text>();
        Debug.Assert(StartGameCount != null);
        NotInRoomText = transform.Find("NotInRoomText").GetComponent<Text>();
        Debug.Assert(NotInRoomText != null);
        
        _currentRoomPresenter.OnInitialize(this);
    }
}
