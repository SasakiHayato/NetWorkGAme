
/// <summary>
/// 生成されたノーツのデータ
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
