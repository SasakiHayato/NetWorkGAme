using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ノーツEditorのデータ管理クラス
/// </summary>

[CreateAssetMenu (fileName = "NotesEditorDatas")]
public class NotesEditorData : ScriptableObject
{
    public List<EditData> EditDatas = new List<EditData>(); 

    [System.Serializable]
    public class EditData
    {
        public string SoundPath;
        public TextAsset DataPath;
    }
}
