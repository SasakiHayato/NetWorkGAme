using System.Collections.Generic;
using UnityEngine;
// Photon 用の名前空間を参照する
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// Photon.Pun.UtilityScripts.PunTurnManager を使った turn-based なゲームを初期化するコンポーネント
/// </summary>
public class NetworkManager : MonoBehaviourPunCallbacks // Photon Realtime 用のクラスを継承する
{
    [SerializeField] ServerSettings _serverSettings;
    
    const string AppIDPun = "58643798-9f22-492d-b25c-6c16c89461bb";

    void Awake()
    {
        _serverSettings.AppSettings.AppIdRealtime = AppIDPun;

        // シーンの自動同期は無効にする
        PhotonNetwork.AutomaticallySyncScene = false;
    }

    void Start()
    {
        if (!GameManager.Instance.IsDebug)
        {
            // Photon に接続する
            Connect("1.0"); // 1.0 はバージョン番号（同じバージョンを指定したクライアント同士が接続できる）
        }
    }

    

    /// <summary>
    /// Photonに接続する
    /// </summary>
    void Connect(string gameVersion)
    {
        if (PhotonNetwork.IsConnected == false)
        {
            PhotonNetwork.GameVersion = gameVersion;    // 同じバージョンを指定したもの同士が接続できる
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    /// <summary>
    /// ロビーに入る
    /// </summary>
    void JoinLobby()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    /// <summary>
    /// 既に存在する部屋に参加する
    /// </summary>
    void JoinExistingRoom()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    /// <summary>
    /// ランダムな名前のルームを作って参加する
    /// </summary>
    void CreateRandomRoom()
    {
        if (PhotonNetwork.IsConnected)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.IsVisible = true;   // 誰でも参加できるようにする
            //roomOptions.MaxPlayers = (byte)_maxPlayerCount;
            PhotonNetwork.CreateRoom(null, roomOptions); // ルーム名に null を指定するとランダムなルーム名を付ける
        }
    }

    /* ***********************************************
     * 
     * これ以降は Photon の Callback メソッド
     * 
     * ***********************************************/

    /// <summary>Photon に接続した時</summary>
    public override void OnConnected()
    {
        Debug.Log("OnConnected");
    }

    /// <summary>Photon との接続が切れた時</summary>
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected");
    }

    /// <summary>マスターサーバーに接続した時</summary>
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        JoinLobby();
    }

    /// <summary>ロビーに参加した時</summary>
    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
        JoinExistingRoom();
    }

    /// <summary>ロビーから出た時</summary>
    public override void OnLeftLobby()
    {
        Debug.Log("OnLeftLobby");
    }

    /// <summary>部屋を作成した時</summary>
    public override void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");
    }

    /// <summary>部屋の作成に失敗した時</summary>
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnCreateRoomFailed: " + message);
    }

    /// <summary>部屋に入室した時</summary>
    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
    }

    /// <summary>指定した部屋への入室に失敗した時</summary>
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRoomFailed: " + message);
    }

    /// <summary>ランダムな部屋への入室に失敗した時</summary>
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed: " + message);
        CreateRandomRoom();
    }

    /// <summary>部屋から退室した時</summary>
    public override void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom");
    }

    /// <summary>自分のいる部屋に他のプレイヤーが入室してきた時</summary>
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("OnPlayerEnteredRoom: " + newPlayer.NickName);
    }

    /// <summary>自分のいる部屋から他のプレイヤーが退室した時</summary>
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("OnPlayerLeftRoom: " + otherPlayer.NickName);
    }

    /// <summary>マスタークライアントが変わった時</summary>
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log("OnMasterClientSwitched to: " + newMasterClient.NickName);
    }

    /// <summary>ロビー情報に更新があった時</summary>
    public override void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
        Debug.Log("OnLobbyStatisticsUpdate");
    }

    /// <summary>ルームリストに更新があった時</summary>
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("OnRoomListUpdate");
    }

    /// <summary>ルームプロパティが更新された時</summary>
    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        Debug.Log("OnRoomPropertiesUpdate");
    }

    /// <summary>プレイヤープロパティが更新された時</summary>
    public override void OnPlayerPropertiesUpdate(Player target, ExitGames.Client.Photon.Hashtable changedProps)
    {
        Debug.Log("OnPlayerPropertiesUpdate");
    }

    /// <summary>フレンドリストに更新があった時</summary>
    public override void OnFriendListUpdate(List<FriendInfo> friendList)
    {
        Debug.Log("OnFriendListUpdate");
    }

    /// <summary>地域リストを受け取った時</summary>
    public override void OnRegionListReceived(RegionHandler regionHandler)
    {
        Debug.Log("OnRegionListReceived");
    }

    /// <summary>WebRpcのレスポンスがあった時</summary>
    public override void OnWebRpcResponse(OperationResponse response)
    {
        Debug.Log("OnWebRpcResponse");
    }

    /// <summary>カスタム認証のレスポンスがあった時</summary>
    public override void OnCustomAuthenticationResponse(Dictionary<string, object> data)
    {
        Debug.Log("OnCustomAuthenticationResponse");
    }

    /// <summary>カスタム認証が失敗した時</summary>
    public override void OnCustomAuthenticationFailed(string debugMessage)
    {
        Debug.Log("OnCustomAuthenticationFailed");
    }
}
