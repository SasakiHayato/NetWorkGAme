using UnityEngine;

/// <summary>
/// Sound‚ð–Â‚ç‚·ƒNƒ‰ƒX
/// </summary>

public class SoundEffect : MonoBehaviour, IPool
{
    AudioSource _source;

    public bool IsUse { get; set; }

    public void SetUp(Transform parent)
    {
        _source = gameObject.AddComponent<AudioSource>();
        IsUse = false;
    }

    void Update()
    {
        if (!IsUse || _source == null) return;

        if (!_source.isPlaying) Delete();
    }

    public void SetData(SoundDataBase.SoundData soundData)
    {
        _source.clip = soundData.AudioClip;
        _source.volume = soundData.Volume;

        if (soundData.SoundType == SoundType.BGM) _source.loop = true;
        else _source.loop = false;

        _source.Play();
    }

    public void Delete()
    {
        IsUse = false;
        _source = null;
    }
}
