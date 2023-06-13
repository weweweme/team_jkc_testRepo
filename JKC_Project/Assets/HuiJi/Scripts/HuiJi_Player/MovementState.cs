using System.Collections;
using System.Collections.Generic;
using Literal;
using UnityEngine;

public class MovementState : StateMachineBehaviour
{
    private PlayerInput _playerInput;
    private PlayerMove _playerMove;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerInput = animator.GetComponent<PlayerInput>();
        _playerMove = animator.GetComponent<PlayerMove>();
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerMove.Move();

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
