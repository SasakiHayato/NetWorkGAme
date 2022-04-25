using UnityEngine;
// Photon 用の名前空間を参照する
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// Netに繋いだ際のゲーム管理クラス
/// </summary>

public class MultiManager : MonoBehaviour
{
    [SerializeField] int _gamePlayer;

    bool _isMaster;

    PhotonView _view;

    public void SetUp()
    {
        _isMaster = PhotonNetwork.IsMasterClient;
        _view = GetComponent<PhotonView>();
    }

    public void LeftCurrentRoom()
    {
        if (_isMaster) return;

        BaseUI.Instance.ParentActive("Matching", false);
        PhotonNetwork.Disconnect();
    }

    public void CheckPlayerCount()
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
        GameManager.Instance.NetworkManager.CallBackRaiseEvent(ReceiverGroup.All, GameSate.Start);
    }
}
