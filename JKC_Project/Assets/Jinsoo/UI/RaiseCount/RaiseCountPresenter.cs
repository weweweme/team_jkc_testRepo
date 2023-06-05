using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class RaiseCountPresenter : Presenter
{
    private RaiseCountView _raiseCountView;
    
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    public override void OnInitialize(View view)
    {
        _raiseCountView = view as RaiseCountView;
        
        InitializeRx();
    }

    /// <summary>
    /// 버튼을 눌렀을때 생성되는 스트림
    /// RaiseCountModel을 업데이트 해준다.
    /// </summary>
    protected override void OnOccuredUserEvent()
    {
        // Add Button을 눌렀을때
        _raiseCountView.AddButton.onClick
            .AsObservable()
            .Subscribe(_ => Model.RaiseCountModel.RaiseCount())
            .AddTo(_compositeDisposable);
        
        // Reset Button을 눌렀을때
        _raiseCountView.ResetButton.onClick
            .AsObservable()
            .Subscribe(_ => Model.RaiseCountModel.ResetCount())
            .AddTo(_compositeDisposable);
    }

    /// <summary>
    /// RaiseCountModel에 따라 View 업데이트
    /// </summary>
    protected override void OnUpdatedModel()
    {
        Model.RaiseCountModel.CurrentCount.SubscribeToText(_raiseCountView.CountText);
    }
    
    public override void OnRelease()
    {
        _raiseCountView = default;
        _compositeDisposable.Dispose();
    }
}
