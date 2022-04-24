using UnityEngine;
using System.Collections.Generic;
using System.Linq;
// Photon 用の名前空間を参照する
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// Botの管理クラス
/// </summary>

public class BotManager : MonoBehaviour, IManager
{
    float _timer;

    BotPlayer _botPlayer;
    List<NotesData> _notesList;

    public class NotesData
    {
        public FieldNotesDataBase.ObjectType ObjectType;
        public Vector2 NotesPosition;
    }

    public NotesData FirstListType
    {
        get
        {
            if (_notesList.Count <= 0) return default;
            else return _notesList.First();
        }
    }

    public float CreateNotesTime { get; set; }
    public Vector2 JudgePos { get; set; }

    public NotesResponsible.NotesProbabilityData NotesProbabilityData { get; set; }

    void Start()
    {
        _notesList = new List<NotesData>();
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

        NotesData notesData = new NotesData();
        notesData.ObjectType = GetProbabilityData((int)pacent);


        _notesList.Add(notesData); 
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
    public GameObject ManagerObject() => gameObject;
    public string ManagerPath() => GetType().Name;
}
