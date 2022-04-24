using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SoundëSëÃÇÃä«óùÉNÉâÉX
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

    ObjectPool<SoundEffect> _soundEffect;
    SoundEffect _bgmEffect = null;

    public void SetUp()
    {
        _soundEffect = new ObjectPool<SoundEffect>();
        _soundEffect.SetUp(_soundPrefab, transform);
    }

    public void Request(string path)
    {
        SoundDataBase.SoundData soundData = _soundDataBase.GetData(path);
        SoundSet(soundData);
    }

    public void Request(int id)
    {
        SoundDataBase.SoundData soundData = _soundDataBase.GetData(id);
        SoundSet(soundData);
    }

    public void StopBGM()
    {
        if (_bgmEffect == null) return;

        _bgmEffect.Delete();
    }

    void SoundSet(SoundDataBase.SoundData soundData)
    {
        SoundEffect soundEffect = _soundEffect.Respons();

        if (soundData.SoundType == SoundType.BGM)
        {
            if (_bgmEffect == null)
            {
                _bgmEffect = soundEffect;
            }
            else
            {
                _bgmEffect.Delete();
                _bgmEffect = soundEffect;
            }
        }

        soundEffect.SetData(soundData);
    }
}
