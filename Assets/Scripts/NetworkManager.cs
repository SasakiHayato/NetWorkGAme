using System.Collections.Generic;
using UnityEngine;
// Photon �p�̖��O��Ԃ��Q�Ƃ���
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// Photon.Pun.UtilityScripts.PunTurnManager ���g���� turn-based �ȃQ�[��������������R���|�[�l���g
/// </summary>
public class NetworkManager : MonoBehaviourPunCallbacks // Photon Realtime �p�̃N���X���p������
{
    [SerializeField] ServerSettings _serverSettings;

    // Note. AppID_Pun
    const string _appID = "58643798-9f22-492d-b25c-6c16c89461bb";

    void Awake()
    {
        GameManager gameManager = new GameManager();
        GameManager.SetInstance(gameManager, gameManager);

        _serverSettings.AppSettings.AppIdRealtime = _appID;

        // �V�[���̎��������͖����ɂ���
        PhotonNetwork.AutomaticallySyncScene = false;
    }

    void Start()
    {
        // Photon �ɐڑ�����
        Connect("1.0"); // 1.0 �̓o�[�W�����ԍ��i�����o�[�W�������w�肵���N���C�A���g���m���ڑ��ł���j
    }

    /// <summary>
    /// Photon�ɐڑ�����
    /// </summary>
    void Connect(string gameVersion)
    {
        if (PhotonNetwork.IsConnected == false)
        {
            PhotonNetwork.GameVersion = gameVersion;    // �����o�[�W�������w�肵�����̓��m���ڑ��ł���
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    /// <summary>
    /// ���r�[�ɓ���
    /// </summary>
    void JoinLobby()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    /// <summary>
    /// ���ɑ��݂��镔���ɎQ������
    /// </summary>
    void JoinExistingRoom()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    /// <summary>
    /// �����_���Ȗ��O�̃��[��������ĎQ������
    /// </summary>
    void CreateRandomRoom()
    {
        if (PhotonNetwork.IsConnected)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.IsVisible = true;   // �N�ł��Q���ł���悤�ɂ���
            //roomOptions.MaxPlayers = (byte)_maxPlayerCount;
            PhotonNetwork.CreateRoom(null, roomOptions); // ���[������ null ���w�肷��ƃ����_���ȃ��[������t����
        }
    }

    /* ***********************************************
     * 
     * ����ȍ~�� Photon �� Callback ���\�b�h
     * 
     * ***********************************************/

    /// <summary>Photon �ɐڑ�������</summary>
    public override void OnConnected()
    {
        Debug.Log("OnConnected");
    }

    /// <summary>Photon �Ƃ̐ڑ����؂ꂽ��</summary>
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected");
    }

    /// <summary>�}�X�^�[�T�[�o�[�ɐڑ�������</summary>
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        JoinLobby();
    }

    /// <summary>���r�[�ɎQ��������</summary>
    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
        JoinExistingRoom();
    }

    /// <summary>���r�[����o����</summary>
    public override void OnLeftLobby()
    {
        Debug.Log("OnLeftLobby");
    }

    /// <summary>�������쐬������</summary>
    public override void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");
    }

    /// <summary>�����̍쐬�Ɏ��s������</summary>
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnCreateRoomFailed: " + message);
    }

    /// <summary>�����ɓ���������</summary>
    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
    }

    /// <summary>�w�肵�������ւ̓����Ɏ��s������</summary>
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRoomFailed: " + message);
    }

    /// <summary>�����_���ȕ����ւ̓����Ɏ��s������</summary>
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed: " + message);
        CreateRandomRoom();
    }

    /// <summary>��������ގ�������</summary>
    public override void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom");
    }

    /// <summary>�����̂��镔���ɑ��̃v���C���[���������Ă�����</summary>
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("OnPlayerEnteredRoom: " + newPlayer.NickName);
    }

    /// <summary>�����̂��镔�����瑼�̃v���C���[���ގ�������</summary>
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("OnPlayerLeftRoom: " + otherPlayer.NickName);
    }

    /// <summary>�}�X�^�[�N���C�A���g���ς������</summary>
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log("OnMasterClientSwitched to: " + newMasterClient.NickName);
    }

    /// <summary>���r�[���ɍX�V����������</summary>
    public override void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
        Debug.Log("OnLobbyStatisticsUpdate");
    }

    /// <summary>���[�����X�g�ɍX�V����������</summary>
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("OnRoomListUpdate");
    }

    /// <summary>���[���v���p�e�B���X�V���ꂽ��</summary>
    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        Debug.Log("OnRoomPropertiesUpdate");
    }

    /// <summary>�v���C���[�v���p�e�B���X�V���ꂽ��</summary>
    public override void OnPlayerPropertiesUpdate(Player target, ExitGames.Client.Photon.Hashtable changedProps)
    {
        Debug.Log("OnPlayerPropertiesUpdate");
    }

    /// <summary>�t�����h���X�g�ɍX�V����������</summary>
    public override void OnFriendListUpdate(List<FriendInfo> friendList)
    {
        Debug.Log("OnFriendListUpdate");
    }

    /// <summary>�n�惊�X�g���󂯎������</summary>
    public override void OnRegionListReceived(RegionHandler regionHandler)
    {
        Debug.Log("OnRegionListReceived");
    }

    /// <summary>WebRpc�̃��X�|���X����������</summary>
    public override void OnWebRpcResponse(OperationResponse response)
    {
        Debug.Log("OnWebRpcResponse");
    }

    /// <summary>�J�X�^���F�؂̃��X�|���X����������</summary>
    public override void OnCustomAuthenticationResponse(Dictionary<string, object> data)
    {
        Debug.Log("OnCustomAuthenticationResponse");
    }

    /// <summary>�J�X�^���F�؂����s������</summary>
    public override void OnCustomAuthenticationFailed(string debugMessage)
    {
        Debug.Log("OnCustomAuthenticationFailed");
    }
}
