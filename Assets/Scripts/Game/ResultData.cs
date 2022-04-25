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
    public enum JudgeType
    {
        Win,
        Lose,
        Dorrow,
    }

    object[] _data;

    int _otherScore;

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
        ManagerPhotonView.RPC("GetData", RpcTarget.Others, _data);
    }

    [PunRPC]
    void GetData(object[] data)
    {
        _otherScore = (int)data[0];
        BaseUI.Instance.CallBack("Result", "OtherResultDisplay", data);
    }

    public void Judge()
    {
        JudgeType type;
        if ((int)_data[0] > _otherScore)
        {
            type = JudgeType.Win;
        }
        else if ((int)_data[0] < _otherScore)
        {
            type = JudgeType.Lose;
        }
        else
        {
            type = JudgeType.Dorrow;
        }

        BaseUI.Instance.CallBack("Result", "ResultJudgeDisplay", new object[] { type });
    }

    // IManager
    public GameObject ManagerObject() => gameObject;
    public string ManagerPath() => GetType().Name;
}
