using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReferenceManager : MonoBehaviour
{
    private PlayerInput _playerInput;
    private CameraAngle _cameraAngle;
    private PlayerMove _playerMove;
    private Transform _playerCharacter;

    private void Awake()
    {
        _playerInput = GetComponentInChildren<PlayerInput>();
        _cameraAngle = GetComponentInChildren<CameraAngle>();
        _playerMove = GetComponentInChildren<PlayerMove>();
        _playerCharacter = transform.Find("Character");
    }

    private void Start()
    {
        _cameraAngle.BindPlayerData(_playerInput, _playerCharacter);
        _playerMove.BindCameraAngle(_cameraAngle);
    }
}
