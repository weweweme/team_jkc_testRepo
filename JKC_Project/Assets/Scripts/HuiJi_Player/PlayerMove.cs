using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _playerRigidbody;
    
    [SerializeField] private float _diveForce;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        DiveStartState.OnDive -= ActivateDiveMove;
        DiveStartState.OnDive += ActivateDiveMove;
    }

    void ActivateDiveMove()
    {
        DiveMove().Forget();
    }

    private async UniTaskVoid DiveMove()
    {
        await UniTask.DelayFrame(3);
        
        // Player가 바라보고 있는 방향을 구한뒤 Dive Direction을 구한다.
        Vector3 _playerDirection = transform.forward;
        Vector3 diveDirection = new Vector3(_playerDirection.x, 1.5f, _playerDirection.z);
        
        // Dive Direction으로 힘을 준다.
        _playerRigidbody.AddForce(diveDirection * _diveForce, ForceMode.Impulse);
        
    }
}
