using UnityEngine;

/// <summary>
/// ゲームフィールド内の全体管理クラス
/// </summary>

public class FieldManager : MonoBehaviour
{
    [SerializeField] FieldNotesDataBase _notesDatas;
    [SerializeField] string _isDebugObjectDataPath;
    [SerializeField] float _playSecondTime;
    [SerializeField] NotesResponsible.NotePostionMasterData _postionData;

    NotesResponsible _notesResponsible;

    void Start()
    {
        _notesResponsible = new NotesResponsible(_notesDatas, _postionData, _isDebugObjectDataPath);
        _notesResponsible.Create();
    }

    void Update()
    {
        _notesResponsible.NotesUpDate();
    }
}