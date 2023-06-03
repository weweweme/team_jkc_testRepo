using UnityEngine.UI;
using UnityEngine;

public class LobbyView : View
{
    public Button EnteredGameButton { get; private set; }
    public InputField InputPlayerName { get; private set; }
    public CurrentRoomView CurrentRoomView { get; private set; }

    private void Awake()
    {
        EnteredGameButton = transform.Find("EnteredGameButton").GetComponent<Button>();
        Debug.Assert(EnteredGameButton != null);
        InputPlayerName = transform.Find("InputPlayerName").GetComponent<InputField>();
        Debug.Assert(InputPlayerName != null);
        CurrentRoomView = transform.Find("CurrentRoomView").GetComponent<CurrentRoomView>();
        Debug.Assert(CurrentRoomView != null);
        CurrentRoomView.gameObject.SetActive(false);
    }
}
