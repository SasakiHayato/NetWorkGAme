using UnityEngine;

/// <summary>
/// ゲームフィールド内の全体管理クラス
/// </summary>

public class FieldManager : MonoBehaviour
{
    [SerializeField] FieldNotesDataBase _fieldNotesDatas;
    [SerializeField] string _isDebugObjectDataPath;
    [SerializeField] float _playSecondTime;
    [SerializeField] NotesResponsible.NotePostionMasterData _postionData;
    [SerializeField] NotesJudgement.NotesJudgeDistData _notesJudgeDistData;

    float _timer;

    NotesResponsible _notesResponsible;
    NotesJudgement _notesJudgement;

    void Start()
    {
        _notesResponsible = new NotesResponsible(_fieldNotesDatas, _postionData, _isDebugObjectDataPath);
        _notesJudgement = new NotesJudgement(_notesJudgeDistData);

        _timer = 0;
    }

    void Update()
    {
        CreateNote();
        _notesResponsible.NotesUpDate();
    }

    void CreateNote()
    {
        _timer += Time.deltaTime;

        if (_timer > 0.5f)
        {
            _timer = 0;
            _notesResponsible.Create();
        }
    }

    public void JudgeNotes()
    {
        if (_notesResponsible.NotesDistance == default) return;

        bool isJudge;
        _notesJudgement.Judge(_notesResponsible.NotesDistance, out isJudge);
        
        if (isJudge) _notesResponsible.Delete();
    }
}