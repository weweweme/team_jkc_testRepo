using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Photon.Pun;
using UniRx;
using UnityEngine;

public class CurrentRoomPresenter : Presenter
{
    private CurrentRoomView _currentRoomView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _currentRoomView = view as CurrentRoomView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
    }

    protected override void OnUpdatedModel()
    {
        if (PhotonNetwork.InRoom)
        {
            InRoom();
        }
        else
        {
            NotInRoom();
            SetActiveCurrentRoomText(false, true);
        }
    }
    
    

    private void InRoom()
    {
        JoinStream();
        SetActiveCurrentRoomText(true, false);
        StartGameCountDown().Forget();
    }

    private void SetCountDownText()
    {
        Debug.Log(Model.LobbyRoomModel.StartCount);
        
        if (Model.LobbyRoomModel.StartCount > 3)
        {
            _currentRoomView.StartGameCount.text = "플레이어를 기다리는 중...";
        }
        else
        {
            _currentRoomView.StartGameCount.text = "이제 게임이 시작됩니다!";
        }
    }

    private async UniTaskVoid StartGameCountDown()
    {
        while (Model.LobbyRoomModel.StartCount > 0)
        {
            await Task.Delay(TimeSpan.FromSeconds(1f));

            Model.LobbyRoomModel.DecreaseStartCount();
        }
    }

    private void SetActiveCurrentRoomText(bool inRoom, bool notInRoom)
    {
        _currentRoomView.AnnouncementText.gameObject.SetActive(inRoom);
        _currentRoomView.CurrentPlayerCount.gameObject.SetActive(inRoom);
        _currentRoomView.StartGameCount.gameObject.SetActive(inRoom);
        _currentRoomView.NotInRoomText.gameObject.SetActive(notInRoom);
    }

    private async UniTaskVoid RetryJoinStream()
    {
        await Task.Delay(TimeSpan.FromSeconds(2f));

        OnUpdatedModel();
    }

    public void JoinStream()
    {
        Observable.EveryUpdate()
            .ObserveEveryValueChanged(_ => Model.LobbyRoomModel.StartCount)
            .Subscribe(_ => SetCountDownText());
        
        Observable.EveryUpdate()
            .ObserveEveryValueChanged(_ => PhotonNetwork.CurrentRoom.PlayerCount)
            .Subscribe(_ => UpdateCurrentPlayerCount());
    }

    private void UpdateCurrentPlayerCount()
    {
        _currentRoomView.CurrentPlayerCount.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
    }

    private void NotInRoom()
    {
        RetryJoinStream().Forget();
        Debug.Log("Entered the room yet...");
    }
    
    public override void OnRelease()
    {
        _currentRoomView = default;
        _compositeDisposable.Dispose();
    }
}
