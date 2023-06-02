using System;
using UniRx;

public class RemainingTimePresenter : Presenter
{
    private RemainingTimeView _remainingTimeView;

    private CompositeDisposable _compositeDisposable = new CompositeDisposable();

    public override void OnInitialize(View view)
    {
        _remainingTimeView = view as RemainingTimeView;
        
        Model.MapModel.Initialize();
        
        InitializeRx();
    }

    /// <summary>
    /// 플레이가 시작되고 1초마다 변경되는 스트림
    /// MapModel을 업데이트 해준다
    /// </summary>
    protected override void OnOccuredUserEvent()
    {
        Observable.Interval(TimeSpan.FromSeconds(1f))
            .Subscribe(_ => Model.MapModel.DecreaseTime());
    }
    
    /// <summary>
    /// MapModel에 따라 View 업데이트
    /// </summary>
    protected override void OnUpdatedModel()
    {
        Observable.EveryUpdate()
            .ObserveEveryValueChanged(_ => Model.MapModel.RemainingTime)
            .Subscribe(_ => SetCurrentTimeRefresh());
    }

    private void SetCurrentTimeRefresh()
    {
        float minutes = Model.MapModel.RemainingTime / 60;
        float seconds = Model.MapModel.RemainingTime % 60;
        _remainingTimeView.CurrentTimeText.text = $"{minutes.ToString():D2} : {seconds.ToString():D2}";
    }

    /// <summary>
    /// ViewController가 파괴될 때 호출되는 함수
    /// 자원 정리 용도
    /// DateViewController가 파괴될 때 자동으로 호출된다
    /// </summary>
    public override void OnRelease()
    {
        _remainingTimeView = default;
        _compositeDisposable.Dispose();
    }
}
