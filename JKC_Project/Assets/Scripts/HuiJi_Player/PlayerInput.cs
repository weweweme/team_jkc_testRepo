using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public Vector3 InputVec { get; private set; }
    public event Action OnJumpState;
    public event Action OnGrabButtonDown;
    public event Action OnGrabButtonUp;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        InputVec = context.ReadValue<Vector3>();
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        // Jump Button을 눌렀을때 JumpState를 실행한다.
        if (context.started)
        {
            OnJumpState?.Invoke();
        }
    }

    public bool CanceledGrab;
    public void OnGrab(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            CanceledGrab = false;
            _animator.SetBool("IsGrab", true);
            OnGrabButtonDown?.Invoke();
        }

        if (context.canceled)
        {
            CanceledGrab = true;
            _animator.SetBool("IsGrab", false);
            _animator.SetBool("IsGrabSuccess", false);
            OnGrabButtonUp?.Invoke();
        }
    }
}
