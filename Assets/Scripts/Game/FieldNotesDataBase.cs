using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// FieldNotesDataの管理クラス
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
    /// ObjectDataをPathで探す
    /// </summary>
    /// <param name="path">string型</param>
    /// <returns>ObjectData</returns>
    public FieldNotesData GetData(string path)
    {
        return ObjectDatas.FirstOrDefault(o => o.Path == path);
    }

    /// <summary>
    /// ObjectDataをIDで探す
    /// </summary>
    /// <param name="id">int型</param>
    /// <returns>ObjectData</returns>
    public FieldNotesData GetData(int id) => ObjectDatas[id];
}

/// <summary>
/// 一つのObjectData
/// </summary>
[System.Serializable]
public class FieldNotesData
{
    public string Path;
    public FieldNotesDataBase.ObjectType ObjectType;
    public Sprite Sprite;
}
