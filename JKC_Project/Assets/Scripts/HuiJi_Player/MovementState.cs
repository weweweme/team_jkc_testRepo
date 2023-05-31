using System.Collections;
using System.Collections.Generic;
using Literal;
using UnityEngine;

public class MovementState : StateMachineBehaviour
{
    private Rigidbody _playerRigidbody;
    private PlayerInput _playerInput;
    
    [SerializeField] private float _rotSpeed = 5f;
    [SerializeField] private float _moveSpeed;
    
    private Vector3 _zeroVec = Vector3.zero;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerRigidbody = animator.GetComponent<Rigidbody>();
        _playerInput = animator.GetComponent<PlayerInput>();
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 인풋이 있을때만 회전을 한다. 
        if (_playerInput.InputVec != _zeroVec && _playerInput.IsReflect == false)
        {
            _playerRigidbody.velocity = _playerInput.InputVec * _moveSpeed;
            animator.transform.rotation = Quaternion.Lerp(animator.transform.rotation, Quaternion.LookRotation(_playerInput.InputVec), _rotSpeed * Time.deltaTime);    
        }

        // Jump키를 눌렀을때
        if (_playerInput.IsJump)
        {
            //Jump State로 전이
            animator.SetBool(AnimLiteral.ISJUMPING, true);
        }

        // Grab키를 눌렀을때
        if (_playerInput.IsAttempingGrab)
        {
            animator.SetBool(AnimLiteral.ISGRAB, true);
        }

        if (_playerInput.IsDive)
        {
            animator.SetBool(AnimLiteral.ISDIVING, true);
        }
    }
}
