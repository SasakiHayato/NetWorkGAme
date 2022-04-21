using UnityEngine;

/// <summary>
/// ノーツの判定を管理するクラス
/// </summary>

public class NotesJudgement
{
    [System.Serializable]
    public class NotesJudgeDistData
    {
        public float DeadDist;
        public float Parfect;
        public float Great;
        public float Good;
    }

    NotesJudgeDistData _distData;

    public NotesJudgement(NotesJudgeDistData distData)
    {
        _distData = distData;
    }

    public void Judge(float noteDist)
    {
        if (_distData.DeadDist < noteDist) return;

        if (_distData.Parfect > noteDist)
        {
            Debug.Log("Parfect");
        }
        else
        {
            if (_distData.Great > noteDist)
            {
                Debug.Log("Great");
            }
            else
            {
                if (_distData.Good > noteDist)
                {
                    Debug.Log("Good");
                }
                else
                {
                    Debug.Log("Miss");
                }
            }
        }
    }
}
