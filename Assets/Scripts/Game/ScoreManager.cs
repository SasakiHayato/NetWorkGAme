using UnityEngine;
using System.Collections.Generic;
using System.Linq;

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

        _comboCounter.Check(type);
    }

    // IManager
    public Object ManagerObject() => gameObject;
    public string ManagerPath() => GetType().Name;
}
