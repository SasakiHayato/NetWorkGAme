using UnityEngine;
// Photon 用の名前空間を参照する
using ExitGames.Client.Photon;

/// <summary>
/// ゲームフィールド内の全体管理クラス
/// </summary>

public class FieldManager : MonoBehaviour, IManager
{
    [SerializeField] FieldNotesDataBase _fieldNotesDatas;
    [SerializeField] string _isDebugObjectDataPath;
    [SerializeField] float _playSecondTime;
    [SerializeField] NotesResponsible.NotesPostionMasterData _postionData;
    [SerializeField] NotesResponsible.NotesProbabilityData _probabilityData;
    [SerializeField] NotesJudgement.NotesJudgeDistData _notesJudgeDistData;
    

    float _createTimer;
    float _gameTimer;

    NotesResponsible _notesResponsible;
    NotesJudgement _notesJudgement;

    void Start()
    {
        _notesResponsible = new NotesResponsible(_fieldNotesDatas, _postionData, _probabilityData);
        _notesResponsible.DebugNotesPath = _isDebugObjectDataPath;

        _notesJudgement = new NotesJudgement(_notesJudgeDistData, _notesResponsible);

        _createTimer = 0;
    }

    void Update()
    {
        GameTime();

        CreateNote();
        _notesResponsible.NotesUpDate();

        if (_notesJudgement.UpDateJudge()) _notesResponsible.Delete();
    }

    void GameTime()
    {
        _gameTimer += Time.deltaTime;
        BaseUI.Instance.CallBack("Game", "Timer", new object[] { (int)_gameTimer });

        if (_gameTimer > _playSecondTime)
        {
            EventData eventData = new EventData();
            eventData.Code = (byte)GameSate.End;
            GameManager.Instance.OnEvent(eventData);
            return;
        }
    }

    void CreateNote()
    {
        _createTimer += Time.deltaTime;

        if (_createTimer > 0.5f)
        {
            _createTimer = 0;
            _notesResponsible.Create();
        }
    }

    public void JudgeNotes()
    {
        if (_notesResponsible.NotesDistance == default) return;
        
        if (_notesJudgement.ClickJudge()) _notesResponsible.Delete();
    }

    // IManager
    public Object ManagerObject() => gameObject;
    public string ManagerPath() => GetType().Name;
}