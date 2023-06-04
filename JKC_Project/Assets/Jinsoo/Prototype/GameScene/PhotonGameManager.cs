using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonGameManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform[] _spawnPoints;

    [SerializeField] 
    private GameObject _player;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnInitialzeLoadScene;
    }

    private void OnInitialzeLoadScene(Scene scene, LoadSceneMode mode)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            int index = 0; 
            foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
            {
                Vector3 spawnPoint = _spawnPoints[index].position;
                GameObject playerPrefab = Resources.Load<GameObject>("Jinsoo/Capsule");
                PhotonNetwork.Instantiate("Jinsoo/Capsule", spawnPoint, Quaternion.identity);
                ++index;
            }

            index = 0;
        }
    }
}
