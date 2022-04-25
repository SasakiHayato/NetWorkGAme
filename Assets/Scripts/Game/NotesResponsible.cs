using UnityEngine;
using System.Collections.Generic;
using System.Linq;

// 三点の定数計算式参照 https://blog.goo.ne.jp/kano08/e/9354000c0311e9a7a0ab01cca34033a3

/// <summary>
/// FieldNotesの管理クラス
/// </summary>

public class NotesResponsible
{
    [System.Serializable]
    public class NotesPostionMasterData
    {
        public Transform[] SetPoitions;
        public Transform[] CenterPostions;
        public Transform EndPosition;
    }

    [System.Serializable]
    public class NotesProbabilityData
    {
        public int Seed;
        public ProbabilityData[] Datas;

        [System.Serializable]
        public class ProbabilityData
        {
            public FieldNotesDataBase.ObjectType ObjectType;
            public int MinPacent;
            public int MaxParcent;
        }
    }

    public class NotesData
    {
        public GameObject Target;
        public NotesObjectData NotesObjectData;
        public float Timer;
        public float constantA;
        public float constantB;
        public float constantC;
    }

    List<NotesData> _notesDatas;

    FieldNotesDataBase _fieldNotesDatas;
    NotesPostionMasterData _notesPosMasterData;
    NotesProbabilityData _notesProbabilityData;
    
    public string DebugNotesPath { get; set; }

    /// <summary> EndPositionとListの先頭ノーツの距離 </summary>
    public float NotesDistance
    {
        get
        {
            if (_notesDatas.Count <= 0) return default;

            Vector2 notePos = _notesDatas.First().Target.transform.position;
            return Vector2.Distance(notePos, _notesPosMasterData.EndPosition.position);
        }
    }

    public Vector2 EndPostion => _notesPosMasterData.EndPosition.position;

    /// <summary>
    /// Listの先頭のノーツデータ
    /// </summary>
    public NotesData FirstNoteData
    {
        get
        {
            if (_notesDatas.Count <= 0) return null;
            else return _notesDatas.First();
        }
    }

    const float NotesSpeed = 5f;

    /// <summary>
    /// NotesResponsibleの初期化
    /// </summary>
    /// <param name="dataBase">FieldNotesDataBase</param>
    /// <param name="pos">NotePostionData</param>
    /// <param name="pro">デバッグ用のNotesPath</param>
    public NotesResponsible(FieldNotesDataBase dataBase, NotesPostionMasterData pos, NotesProbabilityData pro)
    {
        _fieldNotesDatas = dataBase;
        _notesPosMasterData = pos;
        _notesProbabilityData = pro;
        
        _notesDatas = new List<NotesData>();
        Random.InitState(pro.Seed);
    }

    public void NotesUpDate()
    {
        if (_notesDatas.Count <= 0) return;

        foreach (NotesData data in _notesDatas)
        {
            data.Target.transform.position = SetPostion(data);
        }
    }

    Vector2 SetPostion(NotesData data)
    {
        data.Timer -= Time.deltaTime * NotesSpeed;

        float x = data.Timer;
        float y = (data.constantA * x * x) + (data.constantB * x) + data.constantC;

        return new Vector2(x, y);
    }

    /// <summary>
    /// ノーツの生成
    /// </summary>
    public void Create()
    {
        FieldNotesData notesData;

        if (DebugNotesPath != "")
        {
            notesData = _fieldNotesDatas.GetData(DebugNotesPath);
        }
        else
        {
            float pacent;

            if (GameManager.Instance.FieldManager.IsRemoveObstacle) pacent = 0;
            else pacent = Random.value * 100;
            
            notesData = GetProbabilityData((int)pacent);
        }

        GameObject obj = new GameObject($"{notesData.Path}");
        SpriteRenderer spriteRenderer = obj.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = notesData.Sprite;

        SetNotesData(obj, notesData);
    }

    FieldNotesData GetProbabilityData(int parcent)
    {
        foreach (NotesProbabilityData.ProbabilityData data in _notesProbabilityData.Datas)
        {
            if (data.MinPacent <= parcent && data.MaxParcent >= parcent)
            {
                return _fieldNotesDatas.GetData(data.ObjectType);
            }
        }

        return null;
    }

    void SetNotesData(GameObject target, FieldNotesData fieldNotesData)
    {
        float setPosX = Random.Range(_notesPosMasterData.SetPoitions[0].position.x, _notesPosMasterData.SetPoitions[1].position.x);
        float setPosY = Random.Range(_notesPosMasterData.SetPoitions[0].position.y, _notesPosMasterData.SetPoitions[1].position.y);

        float centerPosX = Random.Range(_notesPosMasterData.CenterPostions[0].position.x, _notesPosMasterData.CenterPostions[1].position.x);
        float centerPosY = Random.Range(_notesPosMasterData.CenterPostions[0].position.y, _notesPosMasterData.CenterPostions[1].position.y);

        Vector2 endPos = _notesPosMasterData.EndPosition.position;

        target.transform.position = new Vector2(setPosX, setPosY);

        NotesData notesData = new NotesData();
        notesData.Target = target;
        notesData.Timer = setPosX;

        NotesObjectData notesObjectData = new NotesObjectData();
        notesObjectData.SetUp(fieldNotesData);
        notesData.NotesObjectData = notesObjectData;

        notesData.constantA = ((setPosY - centerPosY) * (setPosX - endPos.x) - (setPosY - endPos.y) * (setPosX - centerPosX)) / ((setPosX - centerPosX) * (setPosX - endPos.x) * (centerPosX - endPos.x));
        notesData.constantB = (setPosY - centerPosY) / (setPosX - centerPosX) - notesData.constantA * (setPosX + centerPosX);
        notesData.constantC = setPosY - notesData.constantA * setPosX * setPosX - notesData.constantB * setPosX;

        _notesDatas.Add(notesData);
    }

    /// <summary>
    /// ノーツの削除
    /// </summary>
    public void Delete()
    {
        if (_notesDatas.Count <= 0) return;

        Object.Destroy(_notesDatas.First().Target);
        _notesDatas.Remove(_notesDatas.First());
    }

    /// <summary>
    /// 全てのノーツの削除
    /// </summary>
    public void DeleteAll()
    {
        if (_notesDatas.Count <= 0) return;

        _notesDatas.ForEach(n => Object.Destroy(n.Target));
        _notesDatas = new List<NotesData>();
    }
}
