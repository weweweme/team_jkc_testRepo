using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerInput : MonoBehaviour
{
    public Vector3 InputVec { get; private set; }
    public Vector2 ScreenToMousePos { get; private set; }
    
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public event Action OnMovement;
    
    private void Update()
    {
        if (_isMoving)
        {
            OnMovement?.Invoke();
        }
    }

    public bool IsReflect { get; set; }
    private bool _isMoving;
    
    public void OnMove(InputAction.CallbackContext context)
    {
        InputVec = context.ReadValue<Vector3>();
        
        if (context.started)
        {
            _isMoving = true;
        }

        if (context.canceled)
        {
            _isMoving = false;
        }
    }

    public bool IsJump { get; private set; }
    public void OnJump(InputAction.CallbackContext context)
    {
        // Jump Button을 눌렀을때 JumpState를 실행한다.
        if (context.started)
        {
            IsJump = true;
        }

        if (context.canceled)
        {
            IsJump = false;
        }
    }

    public bool IsAttempingGrab { get; private set; }
    public void OnGrab(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsAttempingGrab = true;
        }

        if (context.canceled)
        {
            IsAttempingGrab = false;
            _animator.SetBool("IsGrab", false);
            _animator.SetBool("IsGrabSuccess", false);
        }
    }
    
    public bool IsDive { get; private set; }
    public void OnDive(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsDive = true;
        }

        if (context.canceled)
        {
            IsDive = false;
        }
    }

    public event Action OnMouseMove;
    public void OnMouse(InputAction.CallbackContext context)
    {
        ScreenToMousePos = context.ReadValue<Vector2>();
        OnMouseMove?.Invoke();
    }
}
