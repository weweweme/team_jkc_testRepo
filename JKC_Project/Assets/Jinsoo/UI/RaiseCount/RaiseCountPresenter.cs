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

    protected override void OnOccuredUserEvent()
    {
        // Add Button을 눌렀을때
        _raiseCountView.AddButton.onClick
            .AsObservable()
            .Subscribe(_ => Model.RaiseCountModel.RaiseCount());
    }

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
