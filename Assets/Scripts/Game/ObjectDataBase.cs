using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// ObjectData�̊Ǘ��N���X
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
    /// ObjectData��Path�ŒT��
    /// </summary>
    /// <param name="path">string�^</param>
    /// <returns>ObjectData</returns>
    public ObjectData GetData(string path)
    {
        return ObjectDatas.FirstOrDefault(o => o.Path == path);
    }

    /// <summary>
    /// ObjectData��ID�ŒT��
    /// </summary>
    /// <param name="id">int�^</param>
    /// <returns>ObjectData</returns>
    public ObjectData GetData(int id) => ObjectDatas[id];
}

/// <summary>
/// ���ObjectData
/// </summary>
[System.Serializable]
public class ObjectData
{
    public string Path;
    public ObjectDataBase.ObjectType ObjectType;
    public Sprite Sprite;
}
