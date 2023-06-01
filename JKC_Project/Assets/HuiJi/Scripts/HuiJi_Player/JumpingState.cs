using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : StateMachineBehaviour
{
    // Jump 중일때도 Input값에 따라 움직일 수 있다.
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotSpeed = 5f;
    
    private Rigidbody _playerRigidbody;
    private PlayerInput _playerInput;
    private Vector3 _zeroVec = Vector3.zero;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerRigidbody = animator.GetComponent<Rigidbody>();
        _playerInput = animator.GetComponent<PlayerInput>();
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        
        // 인풋이 있을때만 회전을 한다. 
        if (_playerInput.InputVec != _zeroVec)
        {
            _playerRigidbody.AddForce(_playerInput.InputVec * _moveSpeed, ForceMode.Force);
            animator.transform.rotation = Quaternion.Lerp(animator.transform.rotation, Quaternion.LookRotation(_playerInput.InputVec), _rotSpeed * Time.deltaTime);    
        }
    }
}
