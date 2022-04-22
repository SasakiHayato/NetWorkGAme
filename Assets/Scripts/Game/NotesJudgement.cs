using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// ノーツの判定を管理するクラス
/// </summary>

public class NotesJudgement
{
    /// <summary>
    /// 判定データ
    /// </summary>
    [Serializable]
    public class NotesJudgeDistData
    {
        [Serializable]
        public class Data
        {
            public ScoreType Path;
            public float Dist;
        }

        public float DeadDist;
        public List<Data> Datas = new List<Data>();
    }

    NotesJudgeDistData _distData;

    /// <summary>
    /// NotesJudgementの初期化
    /// </summary>
    /// <param name="distData">NotesJudgeDistData</param>
    public NotesJudgement(NotesJudgeDistData distData)
    {
        _distData = distData;
    }

    public void Judge(float noteDist, out bool isJudge)
    {
        if (_distData.DeadDist < noteDist)
        {
            isJudge = false;
            return;
        }

        isJudge = true;

        foreach (NotesJudgeDistData.Data data in _distData.Datas)
        {
            if (data.Dist > noteDist)
            {
                GameManager.Instance.ScoreManager.Add(data.Path);
                return;
            }
        }
        
        GameManager.Instance.ScoreManager.Add(ScoreType.Miss);
    }
}
