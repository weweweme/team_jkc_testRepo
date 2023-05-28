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

    public float moveForce;
    
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
        FollowPlayerBody();
    }

    // 카메라가 플레이어의 좌표를 따라가는 함수
    private void FollowPlayerBody()
    {
        Vector3 prevPlayerPos = new Vector3(_characterBody.position.x, _characterBody.position.y - 1, _characterBody.position.z);
        _playerCameraArm.position = prevPlayerPos;
    }


    // 인풋 시스템 이용. 카메라 시점을 위한 변수 할당
    public void OnMousePos(InputAction.CallbackContext ctx)
    {
        ScreenToMousePos = ctx.ReadValue<Vector2>();
        
        SetCameraAngle();
    }
    
    // 인풋 시스템 이용. 플레이어의 움직임 변수 할당
    public void OnPlayerMovementDir(InputAction.CallbackContext ctx)
    {
        PrimitiveMoveVec = ctx.ReadValue<Vector3>();

        CurrentMoveDirection();
    }

    // 3인칭 카메라 앵글 구현
    private void SetCameraAngle()
    {
        Vector3 cameraAngle = _playerCameraArm.rotation.eulerAngles;
        // 3인칭 기준으로 위 아래 시야를 rotation해주기 위해서는 x축을 기준으로 회전해야함
        float xValueOfAngle = cameraAngle.x - ScreenToMousePos.y;

        // 원작에서도 카메라의 시점을 위쪽으로 돌릴 수 있는 한계가 존재, 이를 구현
        if (xValueOfAngle < 180f)
        {
            xValueOfAngle = Mathf.Clamp(xValueOfAngle, -1f, 50f);
        }
        // 원작에서도 카메라의 시점을 아래쪽으로 돌릴 수 있는 한계가 존재, 이를 구현
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
    
    // 현재 카메라 시야 기준으로 x, z축 판별 식
    // moveDir를 이용하여 플레이어가 움직인다
    private void CurrentMoveDirection()
    {
        if (PrimitiveMoveVec != zeroVec)
        {
            forwardAngleVec = new Vector3(_playerCameraArm.forward.x, 0f, _playerCameraArm.forward.z).normalized;
            rightAngleVec = new Vector3(_playerCameraArm.right.x, 0f, _playerCameraArm.right.z).normalized;
            moveDir = forwardAngleVec * PrimitiveMoveVec.y + rightAngleVec * PrimitiveMoveVec.x;
        }
    }

    // 부딪힌 순간 플레이어가 velocity에 데이터를 넣지 못하게 만들기 위한 변수
    public bool IsReflect;
    // 인풋 시스템
    private void Movement()
    {
        if (PrimitiveMoveVec != zeroVec && IsReflect == false)
        {
            _rigid.AddForce(moveDir * moveForce,ForceMode.Force);
            
            // _rigid.velocity = moveDir * moveForce;
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
