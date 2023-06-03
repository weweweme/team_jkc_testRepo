using Photon.Pun;
using UniRx;
using UnityEngine;

public class LobbyPresenter : Presenter
{
    private LobbyView _lobbyView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _lobbyView = view as LobbyView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        _lobbyView.EnteredGameButton
            .OnClickAsObservable()
            .Subscribe(_ => EnterWaitingRoom());
    }

    private void EnterWaitingRoom()
    {
        CreateRoomOrJoinRoom();
        SetPlayerNickName();
        SetActiveTrueCurrentRoomCanvas();
    }
    
    private void CreateRoomOrJoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    
    private void SetPlayerNickName()
    {
        PhotonNetwork.NickName = _lobbyView.InputPlayerName.text;
    }

    private void SetActiveTrueCurrentRoomCanvas()
    {
        _lobbyView.CurrentRoomView.gameObject.SetActive(true);
    }

    /// <summary>
    /// 업데이트 할 것이 없어서 비워둠
    /// </summary>
    protected override void OnUpdatedModel()
    {
        
    }
    
    public override void OnRelease()
    {
        _lobbyView = default;        
        _compositeDisposable.Dispose();
    }
}
