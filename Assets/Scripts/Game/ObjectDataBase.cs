using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// ObjectDataの管理クラス
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
    /// ObjectDataをPathで探す
    /// </summary>
    /// <param name="path">string型</param>
    /// <returns>ObjectData</returns>
    public ObjectData GetData(string path)
    {
        return ObjectDatas.FirstOrDefault(o => o.Path == path);
    }

    /// <summary>
    /// ObjectDataをIDで探す
    /// </summary>
    /// <param name="id">int型</param>
    /// <returns>ObjectData</returns>
    public ObjectData GetData(int id) => ObjectDatas[id];
}

/// <summary>
/// 一つのObjectData
/// </summary>
[System.Serializable]
public class ObjectData
{
    public string Path;
    public ObjectDataBase.ObjectType ObjectType;
    public Sprite Sprite;
}
