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

    /// <summary>
    /// ObjectData‚ğObjectType‚Å’T‚·
    /// </summary>
    /// <param name="type">ObjectType</param>
    /// <returns>ObjectData</returns>
    public FieldNotesData GetData(ObjectType type)
    {
        var data = ObjectDatas.Where(o => o.ObjectType == type);
        int random = Random.Range(0, data.Count());

        return data.ElementAt(random);
    }
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
