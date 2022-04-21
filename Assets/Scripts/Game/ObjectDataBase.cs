using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// ObjectData‚ÌŠÇ—ƒNƒ‰ƒX
/// </summary>

[CreateAssetMenu (fileName = "ObjectDatas")]
public class ObjectDataBase : ScriptableObject
{
    public enum ObjectType
    {
        BreakTarget,
        Obstacle,
    }

    [SerializeField] List<ObjectData> ObjectDatas = new List<ObjectData>();

    /// <summary>
    /// ObjectData‚ğPath‚Å’T‚·
    /// </summary>
    /// <param name="path">stringŒ^</param>
    /// <returns>ObjectData</returns>
    public ObjectData GetData(string path)
    {
        return ObjectDatas.FirstOrDefault(o => o.Path == path);
    }

    /// <summary>
    /// ObjectData‚ğID‚Å’T‚·
    /// </summary>
    /// <param name="id">intŒ^</param>
    /// <returns>ObjectData</returns>
    public ObjectData GetData(int id) => ObjectDatas[id];
}

/// <summary>
/// ˆê‚Â‚ÌObjectData
/// </summary>
[System.Serializable]
public class ObjectData
{
    public string Path;
    public ObjectDataBase.ObjectType ObjectType;
    public Sprite Sprite;
}
