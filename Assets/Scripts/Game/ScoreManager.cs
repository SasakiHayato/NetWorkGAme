using UnityEngine;
using System.Collections.Generic;
using System.Linq;
// Photon 用の名前空間を参照する
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

public enum ScoreType
{
    Parfect,
    Great,
    Good,

    Miss,
}

/// <summary>
/// スコアの管理クラス
/// </summary>

public class ScoreManager : MonoBehaviour, IManager
{
    [System.Serializable]
    public class ScoreData
    {
        public ScoreType ScoreType;
        public int Score;
    }

    [SerializeField] int _comboEffectCount;
    [SerializeField] List<ScoreData> _scoreDatas = new List<ScoreData>();

    ComboCounter _comboCounter;

    public int CurrentScore { get; private set; }
    public int CurrentComboCount => _comboCounter.CurrentCount;

    void Start()
    {
        CurrentScore = 0;
        _comboCounter = new ComboCounter(_comboEffectCount);
    }

    public void Add(ScoreType type)
    {
        ScoreData data = _scoreDatas.First(s => s.ScoreType == type);
        CurrentScore += data.Score;

        if (CurrentScore < 0) CurrentScore = 0;

        BaseUI.Instance.CallBack("Game", "Score", new object[] { CurrentScore });
        BaseUI.Instance.CallBack("Game", "Judge", new object[] { type.ToString() });

        _comboCounter.Check(type);
    }

    // IManager
    public PhotonView ManagerPhotonView { get; set; }
    public GameObject ManagerObject() => gameObject;
    public string ManagerPath() => GetType().Name;
}
