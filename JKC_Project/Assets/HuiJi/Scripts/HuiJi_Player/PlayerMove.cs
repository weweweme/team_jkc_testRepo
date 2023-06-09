using System;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// 
/// </summary>
public class PlayerMove : MonoBehaviour
{
    private Rigidbody _playerRigidbody;
    private PlayerInput _playerInput;
    private CameraAngle _camera;
    
    [SerializeField] private float _diveForce;
    [SerializeField] private float _jumpForce;
    
    private Vector3 _zeroVec = Vector3.zero;
    [SerializeField] private float _rotSpeed;
    [SerializeField] private float _moveSpeed;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
    }

    public void BindCameraAngle(CameraAngle cameraAngle)
    {
        _camera = cameraAngle;
    }

    private void Start()
    {
        DiveStartState.OnDive -= ActivateDiveAction;
        DiveStartState.OnDive += ActivateDiveAction;

        DiveGetUpState.OnDiveGetUp -= ActivateGetUp;
        DiveGetUpState.OnDiveGetUp += ActivateGetUp;

        JumpStartState.OnJump -= ActivateJumpAction;
        JumpStartState.OnJump += ActivateJumpAction;

        RecoveryState.OnRecoveryState -= ActivateRecovery;
        RecoveryState.OnRecoveryState += ActivateRecovery;

        _playerInput.OnMovement -= CurrentMoveDirection;
        _playerInput.OnMovement += CurrentMoveDirection;
    }
    
    private Vector3 _forwardAngleVec;
    private Vector3 _rightAngleVec;
    private Vector3 _moveDir;
    
    // 현재 카메라 시야 기준으로 x, z축 판별 식
    // moveDir를 이용하여 플레이어가 움직인다
    private void CurrentMoveDirection()
    {
        _forwardAngleVec = new Vector3(_camera.transform.forward.x, 0f, _camera.transform.forward.z).normalized;
        _rightAngleVec = new Vector3(_camera.transform.right.x, 0f, _camera.transform.right.z).normalized;
        _moveDir = _forwardAngleVec * _playerInput.InputVec.z + _rightAngleVec * _playerInput.InputVec.x;
    }

    // 평지이동
    public void Move()
    {
        // 인풋이 있을때만 회전을 한다. 
        if (_playerInput.InputVec != _zeroVec && _playerInput.IsReflect == false)
        {
            // Debug.Log($"moveDir : {moveDir}");
            _playerRigidbody.velocity = _moveDir * _moveSpeed;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_moveDir), _rotSpeed * Time.deltaTime);    
        }
    }

    [SerializeField] private float _jumpMovementForce;
    public void OnJumping()
    {
        // 인풋이 있을때만 회전을 한다. 
        if (_playerInput.InputVec != _zeroVec && _playerInput.IsReflect == false)
        {
            _playerRigidbody.AddForce(_moveDir * _jumpMovementForce, ForceMode.Force);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_moveDir), _rotSpeed * Time.deltaTime);    
        }
    }

    private void ActivateJumpAction()
    {
        JumpAction().Forget();
    }

    // 위로 Jump한다.
    private async UniTaskVoid JumpAction()
    {
        _playerRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    void ActivateDiveAction()
    {
        DiveAction().Forget();
    }

    private Vector3 _playerDirection;
    private Vector3 _diveDirection;
    
    // Dive시 앞으로 점프한다.
    private async UniTaskVoid DiveAction()
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
    [SerializeField] private float _diveRotationSpeed;
    [SerializeField] private float _fallRotationSpeed;
    
    // 캐릭터가 Dive이후 일어나게 하는 함수.
    private async UniTaskVoid GetUp()
    {
        _currentRotation = transform.rotation.eulerAngles;
        _targetRotation = Quaternion.Euler(0, _currentRotation.y, _currentRotation.z);
        
        while (Quaternion.Angle(transform.rotation, _targetRotation) > 0.1f)
        {
            // 캐릭터의 원래 방향으로 rotation한다.
            float step = _diveRotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, step);
            
            await UniTask.Yield();
        }
    }

    // 평지에서 Fall 이후 다시 일어나게 하는 함수.
    void ActivateRecovery()
    {
        Recovery().Forget();
    }
    
    private async UniTaskVoid Recovery()
    {
        _playerInput.IsReflect = true;
        
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        
        _currentRotation = transform.rotation.eulerAngles;
        _targetRotation = Quaternion.Euler(0, _currentRotation.y, 0);
        
        _playerRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        
        Debug.Log($"Fall Rotation : {_currentRotation}");
        // Debug.Break();
        
        while (Quaternion.Angle(transform.rotation, _targetRotation) > 0.1f)
        {
            // 캐릭터의 원래 방향으로 rotation한다.
            float step = _fallRotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, step);
            
            await UniTask.Yield();
        }
        
        _playerInput.IsReflect = false;
    }
    
    private Vector3 tmp;

    private void FixedUpdate()
    {
        // _playerRigidbody.velocity = tmp;
        // _playerRigidbody.velocity = _inputVector * tmpVector;
        
    }

    void MoveMoveGO()
    {
        // Move
        // tmp = Vector3 inputVec * moveSpeed;
    }

    void SetJumpSpeed()
    {
        // moveSpeed = 0.5;
    }

    void SetCommonSpeed()
    {
        // moveSpeed = 1
    }

    void SetDiveSpeed()
    {
        // moveSpeed = 0
    }
    
}
