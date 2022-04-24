using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Photon �p�̖��O��Ԃ��Q�Ƃ���
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// Net�Ɍq�����ۂ̃Q�[���Ǘ��N���X
/// </summary>

public class MultiManager : MonoBehaviour
{
    [SerializeField] int _gamePlayer;

    bool _isMaster;
    
    public void SetUp()
    {
        _isMaster = PhotonNetwork.IsMasterClient;
    }

    public void AddPlayer()
    {
        if (!_isMaster) return;
        
        if (_gamePlayer == PhotonNetwork.PlayerList.Length)
        {
            ClosedRoom();
        }
    }

    void ClosedRoom()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        
    }
}