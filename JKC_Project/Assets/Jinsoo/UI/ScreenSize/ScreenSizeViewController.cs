using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSizeViewController : ViewController
{
    /// <summary>
    /// ScreenSizeView와 ScreenSizePresenter 참조 연결
    /// 연결된 참조로 ViewController에서 상속받은 protected Start() 내용 호출
    /// Start에서는 Presenter.OnInitialize(View) 실행
    /// </summary>
    private void Awake()
    {
        View = transform.Find("ScreenSizeView").GetComponent<ScreenSizeView>();
        Debug.Assert(View != null);
        Presenter = new ScreenSizePresenter();
        Debug.Assert(Presenter != null);
    }
}
