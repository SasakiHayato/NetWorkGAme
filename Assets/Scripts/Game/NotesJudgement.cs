using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// �m�[�c�̔�����Ǘ�����N���X
/// </summary>

public class NotesJudgement
{
    /// <summary>
    /// ����f�[�^
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
    /// NotesJudgement�̏�����
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
