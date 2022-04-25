using UnityEngine;
// Photon 用の名前空間を参照する
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// ゲームフィールド内の全体管理クラス
/// </summary>

public class FieldManager : MonoBehaviour, IManager
{
    [SerializeField] FieldNotesDataBase _fieldNotesDatas;
    [SerializeField] string _isDebugObjectDataPath;
    [SerializeField] int _playSecondTime;
    [SerializeField] NotesResponsible.NotesPostionMasterData _postionData;
    [SerializeField] NotesResponsible.NotesProbabilityData _probabilityData;
    [SerializeField] NotesJudgement.NotesJudgeDistData _notesJudgeDistData;
    
    float _createTimer;
    float _gameTimer;
    
    NotesResponsible _notesResponsible;
    NotesJudgement _notesJudgement;

    public bool IsRemoveObstacle { get; set; }

    const float CretaeTime = 0.5f;

    void Start()
    {
        _notesResponsible = new NotesResponsible(_fieldNotesDatas, _postionData, _probabilityData);
        _notesResponsible.DebugNotesPath = _isDebugObjectDataPath;

        _notesJudgement = new NotesJudgement(_notesJudgeDistData, _notesResponsible);

        _createTimer = 0;
        IsRemoveObstacle = false;

        if (GameManager.Instance.IsUsingBot)
        {
            GameManager.Instance.BotManager.CreateNotesTime = CretaeTime;
            GameManager.Instance.BotManager.NotesProbabilityData = _probabilityData;
            GameManager.Instance.BotManager.JudgePos = _notesResponsible.EndPostion;
        }
    }

    void Update()
    {
        GameTime();

        CreateNotes();
        _notesResponsible.NotesUpDate();

        if (_notesJudgement.UpDateJudge()) _notesResponsible.Delete();
    }

    void GameTime()
    {
        _gameTimer += Time.deltaTime;
        BaseUI.Instance.CallBack("Game", "Timer", new object[] { _playSecondTime - (int)_gameTimer });

        if (_gameTimer > _playSecondTime)
        {
            End();
        }
    }

    void End()
    {
        _notesResponsible.DeleteAll();

        EventData eventData = new EventData();
        eventData.Code = (byte)GameSate.End;
        GameManager.Instance.OnEvent(eventData);
    }

    void CreateNotes()
    {
        _createTimer += Time.deltaTime;

        if (_createTimer > CretaeTime)
        {
            _createTimer = 0;
            _notesResponsible.Create();
        }
    }

    public void JudgeNotes()
    {
        if (_notesResponsible.NotesDistance == default) return;

        NotesObjectData notesObjectData = _notesResponsible.FirstNoteData.NotesObjectData;
        
        if (_notesJudgement.IsCleckJudge)
        {
            _notesJudgement.TypeJudge(notesObjectData);
            _notesResponsible.Delete();
        }
    }

    // IManager
    public PhotonView ManagerPhotonView { get; set; }
    public GameObject ManagerObject() => gameObject; 
    public string ManagerPath() => GetType().Name;
}