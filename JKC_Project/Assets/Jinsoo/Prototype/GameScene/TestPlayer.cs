using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayer : MonoBehaviourPun
{
    private Rigidbody _rigid;
    private float _moveSpeed = 0.7f;
    
    public Vector3 PrimitiveMoveVec { get; private set; }

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 moveDir = PrimitiveMoveVec * _moveSpeed;
        _rigid.AddForce(moveDir, ForceMode.Impulse);
    }

    // 인풋 시스템 이용. 플레이어의 움직임 변수 할당
    public void OnPlayerMovementDir(InputAction.CallbackContext ctx)
    {
        if (photonView.IsMine)
        {
            PrimitiveMoveVec = ctx.ReadValue<Vector3>();
        }
    }
}
