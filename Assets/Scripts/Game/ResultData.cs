using UnityEngine;
// Photon �p�̖��O��Ԃ��Q�Ƃ���
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// UnGame�I������Data�Ǘ��N���X
/// </summary>

public class ResultData : MonoBehaviour, IManager
{
    int _score;
    int _comboCount;

    public void SetData()
    {
        _score = GameManager.Instance.ScoreManager.CurrentScore;
        _comboCount = GameManager.Instance.ScoreManager.CurrentComboCount;

        object[] data = { _score, _comboCount };
        BaseUI.Instance.CallBack("Result", "ResultDisplay", data);
        BaseUI.Instance.CallBack("Result", "ResultAnimation", new object[] { false });
    }

    // IManager
    public PhotonView ManagerPhotonView { get; set; }
    public GameObject ManagerObject() => gameObject;
    public string ManagerPath() => GetType().Name;
}
