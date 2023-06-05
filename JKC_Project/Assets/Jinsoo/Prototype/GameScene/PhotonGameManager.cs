using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PhotonGameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform[] _spawnPoints;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnInitialzeLoadScene;
    }

    private void OnInitialzeLoadScene(Scene scene, LoadSceneMode mode)
    {
        Player player = PhotonNetwork.LocalPlayer;
        object indexObject;

        if (player.CustomProperties.TryGetValue("PersonalIndex", out indexObject))
        {
            int index = (int)indexObject;
        
            Vector3 spawnPoint = _spawnPoints[index].position;
            GameObject playerPrefab = Resources.Load<GameObject>("Jinsoo/Capsule");
            PhotonNetwork.Instantiate("Jinsoo/Capsule", spawnPoint, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Failed to get the player index from custom properties.");
        }
    }
}