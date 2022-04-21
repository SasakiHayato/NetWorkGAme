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

    NotesResponsible _notesResponsible;
    NotesJudgement _notesJudgement;

    void Start()
    {
        _notesResponsible = new NotesResponsible(_fieldNotesDatas, _postionData, _isDebugObjectDataPath);
        _notesJudgement = new NotesJudgement(_notesJudgeDistData);

        _notesResponsible.Create();
    }

    void Update()
    {
        _notesResponsible.NotesUpDate();
    }

    public void JudgeNotes()
    {
        _notesJudgement.Judge(_notesResponsible.NotesDistance);
    }
}