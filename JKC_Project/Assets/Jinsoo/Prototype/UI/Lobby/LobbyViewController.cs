using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("LobbyView").GetComponent<LobbyView>();
        Debug.Assert(View != null);
        Presenter = new LobbyPresenter();
        Debug.Assert(Presenter != null);
    }
}
