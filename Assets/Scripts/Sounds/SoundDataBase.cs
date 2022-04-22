using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum SoundType
{
    BGM,
    SE,
}
/// <summary>
/// Soundのデータベース
/// </summary>

[CreateAssetMenu (fileName = "SoundDatas")]
public class SoundDataBase : ScriptableObject
{
    [SerializeField] List<SoundData> _soundDatas = new List<SoundData>();

    public SoundData GetData(string path)
    {
        return _soundDatas.FirstOrDefault(s => s.Path == path);
    }

    public SoundData GetData(int id) => _soundDatas[id];

    [System.Serializable]
    public class SoundData
    {
        public string Path;
        public SoundType SoundType;
        public AudioClip AudioClip;
        [Range(0, 1)] public float Volume;
    }
}
