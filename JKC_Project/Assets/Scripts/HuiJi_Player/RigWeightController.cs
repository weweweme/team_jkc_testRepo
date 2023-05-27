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
        
        _playerInput.OnGrabButtonDown -= SetNewToken;
        _playerInput.OnGrabButtonDown += SetNewToken;
        _playerInput.OnGrabButtonDown -= CancelDecrease;
        _playerInput.OnGrabButtonDown += CancelDecrease;
        _playerInput.OnGrabButtonDown -= IncreaseWeight;
        _playerInput.OnGrabButtonDown += IncreaseWeight;
     
        _playerInput.OnGrabButtonUp -= CancelIncrease;
        _playerInput.OnGrabButtonUp += CancelIncrease;
        _playerInput.OnGrabButtonUp -= DecreaseWeight;
        _playerInput.OnGrabButtonUp += DecreaseWeight;
 
    }
    
    private void IncreaseWeight()
    {
        // Weight를 점차 올려준다.
        IncreaseWeightGradually().Forget();
    }
    
    
    private float _elapsedTime;
    [SerializeField] private float _duration;
    

    // 토큰 발행
    private CancellationTokenSource _cancelIncrease;
    private CancellationTokenSource _cancelDecrease;

    // Grab키를 누를때마다 취소토큰을 새로 생성한다.
    private void SetNewToken()
    {
        _cancelIncrease = new();
        _cancelDecrease = new();
    }
    
    // Grab버튼을 뗏을때 RaiseWeight를 취소할 이벤트
    void CancelIncrease()
    {
        _cancelIncrease.Cancel();
    }

    // DecreaseWeight가 실행되는 도중 다시 Grab 버튼을 누를 경우 DecreaseWeight를 취소할 이벤트
    void CancelDecrease()
    {
        _cancelDecrease.Cancel();
    }
    
    private async UniTaskVoid IncreaseWeightGradually()
    {
        float currentWeight = _rig.weight;
        
        while (_rig.weight < 1f)
        {
            _elapsedTime += Time.deltaTime;
            // Debug.Log("Grab중");
            _rig.weight = Mathf.Lerp(currentWeight, 1, _elapsedTime / _duration);

            if (!_playerInput.isGrab)
            {
                // duration에서 weight가 증가하는데 걸린 시간을 빼서 감소해야할 남은 시간을 구한다. 
                _elapsedTime = _duration - _elapsedTime;
                Debug.Log($"elaspedTime : {_elapsedTime}");
                await UniTask.Yield(cancellationToken: _cancelDecrease.Token);    
            }
            
            await UniTask.DelayFrame(1);
            // await UniTask.Yield(cancellationToken: _cancelIncrease.Token);
        }

        Debug.Log($"raise elaspedTime : {_elapsedTime}");
        Debug.Log($"raise rig weight : {_rig.weight}");
        _elapsedTime = 0;
    }

    private void DecreaseWeight()
    {
        DecreaseWeightGradually().Forget();
    }
    
    private async UniTaskVoid DecreaseWeightGradually()
    {
        float currentWeight = _rig.weight;
        
        while (_rig.weight > 0f)
        {
            _elapsedTime += Time.deltaTime;
            _rig.weight = Mathf.Lerp(currentWeight, 0, _elapsedTime / _duration);

            if (_playerInput.isGrab)
            {
                // duration에서 weight가 감소하는데 걸린 시간을 빼서 증가해야할 남은 시간을 구한다.
                _elapsedTime = _duration - _elapsedTime;
                await UniTask.Yield(cancellationToken: _cancelDecrease.Token);    
            }
            
            await UniTask.DelayFrame(1);
        }

        Debug.Log($"decrease elaspedTime : {_elapsedTime}");
        Debug.Log($"decrease rig weight : {_rig.weight}");
        _elapsedTime = 0;
    }
}

