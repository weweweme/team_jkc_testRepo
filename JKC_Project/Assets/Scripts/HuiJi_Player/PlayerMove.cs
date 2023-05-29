using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _playerRigidbody;
    
    [SerializeField] private float _diveForce;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        DiveStartState.OnDive -= ActivateDiveMove;
        DiveStartState.OnDive += ActivateDiveMove;

        DiveGetUpState.OnDiveGetUp -= ActivateGetUp;
        DiveGetUpState.OnDiveGetUp += ActivateGetUp;
    }

    void ActivateDiveMove()
    {
        DiveMove().Forget();
    }

    private Vector3 _playerDirection;
    private Vector3 _diveDirection;
    
    // Dive시 앞으로 점프한다.
    private async UniTaskVoid DiveMove()
    {
        await UniTask.DelayFrame(3);
        
        // Player가 바라보고 있는 방향을 구한뒤 Dive Direction을 구한다.
        _playerDirection = transform.forward;
        _diveDirection = new Vector3(_playerDirection.x, 1.5f, _playerDirection.z);
        
        // Dive Direction으로 힘을 준다.
        _playerRigidbody.AddForce(_diveDirection * _diveForce, ForceMode.Impulse);
    }

    // GetUp을 실행시킨다.
    void ActivateGetUp()
    {
        GetUp().Forget();
    }

    private Vector3 _currentRotation;
    private Quaternion _targetRotation;
    [SerializeField] private float _rotationSpeed;
    
    // 캐릭터가 Dive이후 일어나게 하는 함수.
    private async UniTaskVoid GetUp()
    {
        _currentRotation = transform.rotation.eulerAngles;
        _targetRotation = Quaternion.Euler(0, _currentRotation.y, _currentRotation.z);
        
        while (Quaternion.Angle(transform.rotation, _targetRotation) > 0.1f)
        {
            // 캐릭터의 원래 방향으로 rotation한다.
            float step = _rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, step);
            
            await UniTask.Yield();
        }
    }
}
