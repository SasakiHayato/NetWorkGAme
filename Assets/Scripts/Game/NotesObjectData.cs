
/// <summary>
/// �������ꂽ�m�[�c�̃f�[�^
/// </summary>

public class NotesObjectData
{
    FieldNotesData _fieldNotesData;
    public FieldNotesData NotesData => _fieldNotesData;

    public void SetUp(FieldNotesData fieldNotesData)
    {
        _fieldNotesData = fieldNotesData;
    }
}
