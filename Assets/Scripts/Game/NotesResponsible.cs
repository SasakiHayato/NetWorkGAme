using UnityEngine;
using System.Collections.Generic;
using System.Linq;

// �O�_�̒萔�v�Z���Q�� https://blog.goo.ne.jp/kano08/e/9354000c0311e9a7a0ab01cca34033a3

/// <summary>
/// FieldNotes�̊Ǘ��N���X
/// </summary>

public class NotesResponsible
{
    [System.Serializable]
    public class NotePostionMasterData
    {
        public Transform[] SetPoitions;
        public Transform[] CenterPostions;
        public Transform EndPosition;
    }

    class NotesData
    {
        public GameObject Target;
        
        public float constantA;
        public float constantB;
        public float constantC;
    }

    List<NotesData> _noteDatas;

    FieldNotesDataBase _notesDatas;
    NotePostionMasterData _notesPosMasterData;
    string _debugNotesPath;

    /// <summary> EndPosition��List�̐擪�m�[�c�̋��� </summary>
    public float NotesDistance
    {
        get
        {
            if (_noteDatas.Count <= 0) return default;

            Vector2 notePos = _noteDatas.First().Target.transform.position;
            return Vector2.Distance(notePos, _notesPosMasterData.EndPosition.position);
        }
    }

    const float NotesSpeed = 0.02f;

    /// <summary>
    /// NotesResponsible�̏�����
    /// </summary>
    /// <param name="dataBase">FieldNotesDataBase</param>
    /// <param name="postionData">NotePostionData</param>
    /// <param name="debugPath">�f�o�b�O�p��NotesPath</param>
    public NotesResponsible(FieldNotesDataBase dataBase, NotePostionMasterData postionData, string debugPath)
    {
        _notesDatas = dataBase;
        _notesPosMasterData = postionData;
        _debugNotesPath = debugPath;

        _noteDatas = new List<NotesData>();
    }

    public void NotesUpDate()
    {
        if (_noteDatas.Count <= 0) return;

        foreach (NotesData data in _noteDatas)
        {
            data.Target.transform.position = SetPostion(data);
        }
    }

    Vector2 SetPostion(NotesData data)
    {
        float x = data.Target.transform.position.x - NotesSpeed;
        float y = (data.constantA * x * x) + (data.constantB * x) + data.constantC;

        return new Vector2(x, y);
    }

    public void Create()
    {
        FieldNotesData notesData = null;

        if (_debugNotesPath != "")
        {
            notesData = _notesDatas.GetData(_debugNotesPath);
        }

        GameObject obj = new GameObject($"{notesData.Path}");
        SpriteRenderer spriteRenderer = obj.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = notesData.Sprite;

        SetNotesData(obj);
    }

    void SetNotesData(GameObject target)
    {
        float setPosX = Random.Range(_notesPosMasterData.SetPoitions[0].position.x, _notesPosMasterData.SetPoitions[1].position.x);
        float setPosY = Random.Range(_notesPosMasterData.SetPoitions[0].position.y, _notesPosMasterData.SetPoitions[1].position.y);

        float centerPosX = Random.Range(_notesPosMasterData.CenterPostions[0].position.x, _notesPosMasterData.CenterPostions[1].position.x);
        float centerPosY = Random.Range(_notesPosMasterData.CenterPostions[0].position.y, _notesPosMasterData.CenterPostions[1].position.y);

        Vector2 endPos = _notesPosMasterData.EndPosition.position;

        target.transform.position = new Vector2(setPosX, setPosY);

        NotesData notesData = new NotesData();
        notesData.Target = target;
        
        notesData.constantA = ((setPosY - centerPosY) * (setPosX - endPos.x) - (setPosY - endPos.y) * (setPosX - centerPosX)) / ((setPosX - centerPosX) * (setPosX - endPos.x) * (centerPosX - endPos.x));
        notesData.constantB = (setPosY - centerPosY) / (setPosX - centerPosX) - notesData.constantA * (setPosX + centerPosX);
        notesData.constantC = setPosY - notesData.constantA * setPosX * setPosX - notesData.constantB * setPosX;

        _noteDatas.Add(notesData);
    }
}
