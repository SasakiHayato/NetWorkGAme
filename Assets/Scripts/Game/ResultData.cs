using UnityEngine;
// Photon 用の名前空間を参照する
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// UnGame終了時のData管理クラス
/// </summary>

public class ResultData : MonoBehaviour, IManager, INetworkManager
{
    object[] _data;

    // INetworkManager
    public PhotonView ManagerPhotonView { get; set; }

    public void SetMyData()
    {
        int score = GameManager.Instance.ScoreManager.CurrentScore;
        int comboCount = GameManager.Instance.ScoreManager.CurrentComboCount;
        int maxCombo = GameManager.Instance.ScoreManager.MaxComboCount;

        _data = new object[] { score, comboCount, maxCombo };

        BaseUI.Instance.CallBack("Result", "MyResultDisplay", _data);
    }

    public void SendMyData()
    {
        ManagerPhotonView.RPC("SendData", RpcTarget.Others, _data);
    }

    [PunRPC]
    void SendData(object[] data)
    {
        BaseUI.Instance.CallBack("Result", "OtherResultDisplay", data);
    }

    // IManager
    public GameObject ManagerObject() => gameObject;
    public string ManagerPath() => GetType().Name;
}
