using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RigWeightController : MonoBehaviour
{
    private Rig _rig;
    private PlayerInput _playerInput;

    private void Awake()
    {
        _rig = GetComponent<Rig>();
        _playerInput = GetComponentInParent<PlayerInput>();
    }

    private void Start()
    {
        _rig.weight = 0;
        
        _playerInput.OnGrabButtonDown -= SetNewTokken;
        _playerInput.OnGrabButtonDown += SetNewTokken;
        _playerInput.OnGrabButtonDown -= RaiseWeight;
        _playerInput.OnGrabButtonDown += RaiseWeight;
     
        _playerInput.OnGrabButtonUp -= CancelGrab;
        _playerInput.OnGrabButtonUp += CancelGrab;
        _playerInput.OnGrabButtonUp -= DecreaseWeight;
        _playerInput.OnGrabButtonUp += DecreaseWeight;
 
    }
    
    private void RaiseWeight()
    {
        // Weight를 점차 올려준다.
        RaiseWeightUniTask().Forget();
    }
    
    
    private float _elapsedTime;
    [SerializeField] private float _duration;
    

    // 토큰 발행
    private CancellationTokenSource _cancel;

    private void SetNewTokken()
    {
        _cancel = new();
    }
    
    // Grab버튼을 뗏을때 RaiseWeight를 취소할 이벤트
    void CancelGrab()
    {
        _cancel.Cancel();
    }
    private async UniTaskVoid RaiseWeightUniTask()
    {
        while (_elapsedTime <= _duration)
        {
            _elapsedTime += Time.deltaTime;
            Debug.Log("Grab중");
            _rig.weight = Mathf.Lerp(_rig.weight, 1, _elapsedTime / _duration);
            await UniTask.Yield(cancellationToken: _cancel.Token);
        }

        _elapsedTime = 0;
    }

    private void DecreaseWeight()
    {
        DecreaseWeightGradually().Forget();
    }
    
    private async UniTaskVoid DecreaseWeightGradually()
    {
        while (_elapsedTime <= _duration)
        {
            _elapsedTime += Time.deltaTime;
            _rig.weight = Mathf.Lerp(_rig.weight, 0, _elapsedTime / _duration);
            await UniTask.Yield();
        }

        _elapsedTime = 0;
    }
}
