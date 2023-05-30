using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : StateMachineBehaviour
{
    // Jump 중일때도 Input값에 따라 움직일 수 있다.
    [SerializeField] private float _moveSpeed;
    private Rigidbody _playerRigidbody;
    private PlayerInput _playerInput;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerRigidbody = animator.GetComponent<Rigidbody>();
        _playerInput = animator.GetComponent<PlayerInput>();
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerRigidbody.AddForce(_playerInput.InputVec * _moveSpeed, ForceMode.Force);
    }
}
