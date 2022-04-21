using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// FieldNotesData�̊Ǘ��N���X
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
    /// ObjectData��Path�ŒT��
    /// </summary>
    /// <param name="path">string�^</param>
    /// <returns>ObjectData</returns>
    public FieldNotesData GetData(string path)
    {
        return ObjectDatas.FirstOrDefault(o => o.Path == path);
    }

    /// <summary>
    /// ObjectData��ID�ŒT��
    /// </summary>
    /// <param name="id">int�^</param>
    /// <returns>ObjectData</returns>
    public FieldNotesData GetData(int id) => ObjectDatas[id];
}

/// <summary>
/// ���ObjectData
/// </summary>
[System.Serializable]
public class FieldNotesData
{
    public string Path;
    public FieldNotesDataBase.ObjectType ObjectType;
    public Sprite Sprite;
}
