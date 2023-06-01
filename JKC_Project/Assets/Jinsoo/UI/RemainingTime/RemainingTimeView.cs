using System;
using UnityEngine;
using UnityEngine.UI;

public class RemainingTimeView : View
{
    // 현재 시간을 알려주는 텍스트
    public Text CurrentTimeText { get; private set; }

    /// <summary>
    /// 각각의 UI에 접근할 수 있는 참조 연결 
    /// </summary>
    private void Awake()
    {
        CurrentTimeText = transform.Find("CurrentTimeText").GetComponent<Text>();
        Debug.Assert(CurrentTimeText != null);
    }
}
