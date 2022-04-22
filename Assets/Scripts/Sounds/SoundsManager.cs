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

    public float MasterVoume => _mastarVolume;
    public float BGMVolume { get => _bgmVolume; set { _bgmVolume = value; } }
    public float SEVolume { get => _seVolume; set { _seVolume = value; } }

    bool _isSetUp = false;

    ObjectPool<SoundEffect> _soundEffect;

    public void SetUp()
    {
        if (_isSetUp) return;

        _isSetUp = true;

        _soundEffect = new ObjectPool<SoundEffect>();
        _soundEffect.SetUp(_soundPrefab, transform);
    }

    public void Request(string path)
    {
        SoundDataBase.SoundData soundData = _soundDataBase.GetData(path);
        _soundEffect.Respons().SetData(soundData);
    }

    public void Request(int id)
    {
        SoundDataBase.SoundData soundData = _soundDataBase.GetData(id);
        _soundEffect.Respons().SetData(soundData);
    }
}
