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

    NotesResponsible _notesResponsible;
    NotesJudgeDistData _distData;

    float _deadDist;

    /// <summary>
    /// �����Click�������̔���
    /// </summary>
    public bool IsCleckJudge
    {
        get
        {
            if (_distData.DeadDist < _notesResponsible.NotesDistance) return false;
            else return true;
        }
    }
    
    /// <summary>
    /// NotesJudgement�̏�����
    /// </summary>
    /// <param name="distData">NotesJudgeDistData</param>
    public NotesJudgement(NotesJudgeDistData distData, NotesResponsible notesResponsible)
    {
        _distData = distData;
        _notesResponsible = notesResponsible;

        _deadDist = float.MinValue;
        foreach (NotesJudgeDistData.Data data in _distData.Datas)
        {
            if (_deadDist < data.Dist)
            {
                _deadDist = data.Dist;
            }
        }
    }

    /// <summary>
    /// �}�C�t���[���̔���
    /// </summary>
    public bool UpDateJudge()
    {
        if (_notesResponsible.FirstNoteData == null) return false;

        float firstNotesPosX = _notesResponsible.FirstNoteData.Target.transform.position.x;
        if (_notesResponsible.EndPostion.x > firstNotesPosX)
        {
            if (_notesResponsible.NotesDistance > _deadDist)
            {
                NotesObjectData notesData = _notesResponsible.FirstNoteData.NotesObjectData;
                if (notesData.NotesData.ObjectType == FieldNotesDataBase.ObjectType.BreakTarget)
                    GameManager.Instance.ScoreManager.Add(ScoreType.Miss);

                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// �m�[�cType���菈��
    /// </summary>
    /// <param name="objectType">�m�[�c�^�C�v</param>
    public void TypeJudge(NotesObjectData notesObjectData)
    {
        switch (notesObjectData.NotesData.ObjectType)
        {
            case FieldNotesDataBase.ObjectType.BreakTarget:
                ScoreJudge();

                break;
            case FieldNotesDataBase.ObjectType.Obstacle:
                GameManager.Instance.SoundsManager.Request("Obstacle");
                GameManager.Instance.ScoreManager.Add(ScoreType.Miss);

                break;
            case FieldNotesDataBase.ObjectType.Item:
                GameManager.Instance.SoundsManager.Request("UseItem");
                BaseUI.Instance.CallBack("Game", "Judge", new object[] { "Item!!" });
                GameManager.Instance.ItemManager.Request();

                break;
        }
    }

    void ScoreJudge()
    {
        foreach (NotesJudgeDistData.Data data in _distData.Datas)
        {
            if (data.Dist > _notesResponsible.NotesDistance)
            {
                GameManager.Instance.ScoreManager.Add(data.Path);
                return;
            }
        }

        GameManager.Instance.ScoreManager.Add(ScoreType.Miss);
    }
}
