using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody _playerRigidbody;
    private PlayerInput _playerInput;
    
    [SerializeField] private float _rotSpeed = 5f;
    [SerializeField] private float _moveSpeed = 10f;
    
    [SerializeField] private float _jumpForce = 50f;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        _playerInput.OnJumpState -= Jump;
        _playerInput.OnJumpState += Jump;
    }

    private void FixedUpdate()
    {
        Movement();
        CurrentBodyRotation();
    }

    // Player 움직임
    private void Movement()
    {
        // if문의 조건을 체크해주지 않으면 Input이 없을때도 velocity에 영향을 주므로 체크를 해줘야 함.
        if (_playerInput.InputVec.x != 0 || _playerInput.InputVec.z != 0)
        {
            _playerRigidbody.AddForce(_playerInput.InputVec * _moveSpeed, ForceMode.Force);
        }
    }
    
    // Player 회전
    private void CurrentBodyRotation()
    {
        if (_playerInput.InputVec.x != 0 || _playerInput.InputVec.z != 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_playerInput.InputVec), _rotSpeed * Time.deltaTime);    
        }
    }
    
    // Jump
    private void Jump()
    {
        _playerRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }
}
