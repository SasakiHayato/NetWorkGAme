using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sound‘S‘Ì‚ÌŠÇ—ƒNƒ‰ƒX
/// </summary>

public class SoundsManager : MonoBehaviour
{
    [SerializeField] SoundDataBase _soundDataBase;
    [SerializeField] SoundEffect _soundPrefab;
    [SerializeField, Range(0, 1)] float _mastarVolume;
    [SerializeField, Range(0, 1)] float _bgmVolume;
    [SerializeField, Range(0, 1)] float _seVolume;
    
    ObjectPool<SoundEffect> _soundEffect;

    void Start()
    {
        _soundEffect = new ObjectPool<SoundEffect>();
        _soundEffect.SetUp(_soundPrefab, transform);
    }

    public void Request(string path)
    {
        SoundDataBase.SoundData soundData = _soundDataBase.GetData(path);
        _soundEffect.Respons().SetData(soundData);
    }
}
