using UnityEngine;

/// <summary>
/// Sound‚ð–Â‚ç‚·ƒNƒ‰ƒX
/// </summary>

public class SoundEffect : MonoBehaviour, IPool
{
    SoundType _soundType;
    float _soundVolume;
    AudioSource _source;

    public bool IsUse { get; set; }

    public void SetUp(Transform parent)
    {
        _source = gameObject.AddComponent<AudioSource>();
        IsUse = false;
    }

    void Update()
    {
        if (!IsUse) return;

        if (!_source.isPlaying) Delete();

        _source.volume = SetVolume();
    }

    public void SetData(SoundDataBase.SoundData soundData)
    {
        _source.clip = soundData.AudioClip;
        _soundType = soundData.SoundType;
        _soundVolume = soundData.Volume;

        if (soundData.SoundType == SoundType.BGM) _source.loop = true;
        else _source.loop = false;

        _source.Play();
        IsUse = true;
    }

    float SetVolume()
    {
        float volume;
        float masterVolume = GameManager.Instance.SoundsManager.MasterVoume;

        if (_soundType == SoundType.BGM)
        {
            volume = masterVolume * GameManager.Instance.SoundsManager.BGMVolume;
        }
        else
        {
            volume = masterVolume * GameManager.Instance.SoundsManager.SEVolume;
        }

        return _soundVolume * volume;
    }

    public void Delete()
    {
        IsUse = false;
        _soundVolume = 0;
        _soundType = default;
        _source.clip = null;
    }
}
