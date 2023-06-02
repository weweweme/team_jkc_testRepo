using UniRx;
using UnityEngine;

public class ScreenSizePresenter : Presenter
{
    private ScreenSizeView _screenSizeView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _screenSizeView = view as ScreenSizeView;
        
        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
        _screenSizeView.TopButton.OnClickAsObservable()
            .Subscribe(_ => Model.MapModel.SetCurrentScreenSizeIndex(0));
        _screenSizeView.MiddleButton.OnClickAsObservable()
            .Subscribe(_ => Model.MapModel.SetCurrentScreenSizeIndex(1));
        _screenSizeView.BotButton.OnClickAsObservable()
            .Subscribe(_ => Model.MapModel.SetCurrentScreenSizeIndex(2));
    }

    protected override void OnUpdatedModel()
    {
        Observable.EveryUpdate()
            .ObserveEveryValueChanged(_ => Model.MapModel.CurrentScreenSizeIndex)
            .Subscribe(_ => ChangeButtonColor());
    }

    private void ChangeButtonColor()
    {
        UpdateButtonColor(Model.MapModel.PrevScreenSizeIndex, Color.white);
        UpdateButtonColor(Model.MapModel.CurrentScreenSizeIndex, Color.red);
    }
    
    private void UpdateButtonColor(int index, Color color)
    {
        switch (index)
        {
            case 0:
                _screenSizeView.TopButton.image.color = color;
                break;
            
            case 1:
                _screenSizeView.MiddleButton.image.color = color;
                break;
            
            case 2:
                _screenSizeView.BotButton.image.color = color;
                break;
        }
    }

    public override void OnRelease()
    {
        _screenSizeView = default;
        _compositeDisposable.Dispose();
    }
}
