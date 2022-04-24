using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// BotÇÃä«óùÉNÉâÉX
/// </summary>

public class BotManager : MonoBehaviour, IManager
{
    float _timer;

    BotPlayer _botPlayer;
    List<FieldNotesDataBase.ObjectType> _objectTypeList;

    public FieldNotesDataBase.ObjectType FirstListType
    {
        get
        {
            if (_objectTypeList.Count <= 0) return default;
            else return _objectTypeList.First();
        }
    }

    public float CreateNotesTime { get; set; }
    public Vector2 NotesPostion { get; private set; }
    public Vector2 JudgePos { get; set; }

    public NotesResponsible.NotesProbabilityData NotesProbabilityData { get; set; }

    void Start()
    {
        _objectTypeList = new List<FieldNotesDataBase.ObjectType>();
        _botPlayer = new BotPlayer();
    }

    void Update()
    {
        if (GameManager.Instance.CurrentGameState != GameSate.InGame) return;

        _botPlayer.BotUpdate();

        _timer += Time.deltaTime;
        if (_timer > CreateNotesTime)
        {
            _timer = 0;
            CreateNotesData();
        }
    }

    void CreateNotesData()
    {
        float pacent;

        if (GameManager.Instance.FieldManager.IsRemoveObstacle) pacent = 0;
        else pacent = Random.value * 100;

        _objectTypeList.Add(GetProbabilityData((int)pacent)); 
    }

    FieldNotesDataBase.ObjectType GetProbabilityData(int parcent)
    {
        foreach (NotesResponsible.NotesProbabilityData.ProbabilityData data in NotesProbabilityData.Datas)
        {
            if (data.MinPacent <= parcent && data.MaxParcent >= parcent)
            {
                return data.ObjectType;
            }
        }

        return default;
    }

    // IManager
    public Object ManagerObject() => gameObject;
    public string ManagerPath() => GetType().Name;
}
