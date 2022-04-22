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
/// �X�R�A�̊Ǘ��N���X
/// </summary>

public class ScoreManager : MonoBehaviour, IManager
{
    [System.Serializable]
    public class ScoreData
    {
        public ScoreType ScoreType;
        public int Score;
    }

    [SerializeField] List<ScoreData> _scoreDatas = new List<ScoreData>();

    public int CurrentScore { get; private set; }

    void Start()
    {
        CurrentScore = 0;
    }

    public void Add(ScoreType type)
    {
        ScoreData data = _scoreDatas.First(s => s.ScoreType == type);
        CurrentScore += data.Score;

        if (CurrentScore < 0) CurrentScore = 0;

        BaseUI.Instance.CallBack("Game", "Score", new object[] { CurrentScore });
    }

    // IManager
    public Object ManagerObject() => gameObject;
    public string ManagerPath() => GetType().Name;
}
