using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesObjectData
{
    FieldNotesData _fieldNotesData;
    public FieldNotesData NotesData => _fieldNotesData;

    public void SetUp(FieldNotesData fieldNotesData)
    {
        _fieldNotesData = fieldNotesData;
    }
}
