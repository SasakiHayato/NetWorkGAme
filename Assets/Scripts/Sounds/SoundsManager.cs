using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sound全体の管理クラス
/// </summary>

public class SoundsManager : MonoBehaviour
{
    [SerializeField] SoundDataBase _soundDataBase;
    [SerializeField] SoundEffect _soundPrefab;
    
    ObjectPool<SoundEffect> _soundEffect;

    void Start()
    {
        _soundEffect = new ObjectPool<SoundEffect>();
        _soundEffect.SetUp(_soundPrefab);
    }

    public void Request(string path)
    {
        SoundDataBase.SoundData soundData = _soundDataBase.GetData(path);
        _soundEffect.Respons().SetData(soundData);
    }
}
