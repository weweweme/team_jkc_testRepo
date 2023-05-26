using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestInput : MonoBehaviour
{
    private Rigidbody _rigid;
    
    private Transform _playerCameraArm;
    private Transform _characterBody;

    private Vector3 zeroVec = Vector3.zero;
    
    public Vector2 ScreenToMousePos { get; private set; }
    public Vector3 PrimitiveMoveVec { get; private set; }
    
    private void Awake()
    {
        _playerCameraArm = transform.Find("CameraArm");
        _characterBody = transform.Find("CharacterBody");
        _rigid = GetComponentInChildren<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Movement();
        BodyRotation();
        MoveToBody();
    }

    private void MoveToBody()
    {
        Vector3 prevPlayerPos = new Vector3(_characterBody.position.x, _characterBody.position.y - 1, _characterBody.position.z);
        _playerCameraArm.position = prevPlayerPos;
    }


    public void OnMousePos(InputAction.CallbackContext ctx)
    {
        ScreenToMousePos = ctx.ReadValue<Vector2>();
        
        SetCameraAngle();
    }
    
    public void OnPlayerMovementDir(InputAction.CallbackContext ctx)
    {
        PrimitiveMoveVec = ctx.ReadValue<Vector3>();

        CurrentMoveDirection();
    }

    private void SetCameraAngle()
    {
        Vector3 cameraAngle = _playerCameraArm.rotation.eulerAngles;
        float xValueOfAngle = cameraAngle.x - ScreenToMousePos.y;

        if (xValueOfAngle < 180f)
        {
            xValueOfAngle = Mathf.Clamp(xValueOfAngle, -1f, 50f);
        }
        else
        {
            xValueOfAngle = Mathf.Clamp(xValueOfAngle, 345f, 361f);
        }
        
        _playerCameraArm.rotation = Quaternion.Euler(
            xValueOfAngle,
            cameraAngle.y + ScreenToMousePos.x, 
                cameraAngle.z);
    }

    private Vector3 forwardAngleVec;
    private Vector3 rightAngleVec;
    private Vector3 moveDir;
    
    private void CurrentMoveDirection()
    {
        if (PrimitiveMoveVec != zeroVec)
        {
            forwardAngleVec = new Vector3(_playerCameraArm.forward.x, 0f, _playerCameraArm.forward.z).normalized;
            rightAngleVec = new Vector3(_playerCameraArm.right.x, 0f, _playerCameraArm.right.z).normalized;
            moveDir = forwardAngleVec * PrimitiveMoveVec.y + rightAngleVec * PrimitiveMoveVec.x;
        }
    }
    
    private void Movement()
    {
        if (PrimitiveMoveVec != zeroVec)
        {
            _rigid.AddForce(moveDir * 10f,ForceMode.Force);
        }
    }

    private void BodyRotation()
    {
        if (PrimitiveMoveVec != zeroVec)
        {
            _characterBody.rotation = Quaternion.Lerp(_characterBody.rotation, Quaternion.LookRotation(moveDir), 5f * Time.fixedDeltaTime);
        }
    }
}
