using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// FieldNotesData‚ÌŠÇ—ƒNƒ‰ƒX
/// </summary>

[CreateAssetMenu (fileName = "FieldNotesDatas")]
public class FieldNotesDataBase : ScriptableObject
{
    public enum ObjectType
    {
        BreakTarget,
        Obstacle,
        Item,
    }

    [SerializeField] List<FieldNotesData> ObjectDatas = new List<FieldNotesData>();

    /// <summary>
    /// ObjectData‚ğPath‚Å’T‚·
    /// </summary>
    /// <param name="path">stringŒ^</param>
    /// <returns>ObjectData</returns>
    public FieldNotesData GetData(string path)
    {
        return ObjectDatas.FirstOrDefault(o => o.Path == path);
    }

    /// <summary>
    /// ObjectData‚ğID‚Å’T‚·
    /// </summary>
    /// <param name="id">intŒ^</param>
    /// <returns>ObjectData</returns>
    public FieldNotesData GetData(int id) => ObjectDatas[id];
}

/// <summary>
/// ˆê‚Â‚ÌObjectData
/// </summary>
[System.Serializable]
public class FieldNotesData
{
    public string Path;
    public FieldNotesDataBase.ObjectType ObjectType;
    public Sprite Sprite;
}
