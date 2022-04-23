using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesEditorBase : MonoBehaviour, IManager
{
    [SerializeField] NotesEditorData _notesEditorData;
    [SerializeField] SoundDataBase _soundDataBase;

    // IManager
    public Object ManagerObject() => gameObject;
    public string ManagerPath() => GetType().Name;
}
