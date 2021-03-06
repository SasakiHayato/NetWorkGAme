using UnityEngine;

/// <summary>
/// ButtonでのSoundを申請するクラス
/// </summary>

public class SoundSetter : MonoBehaviour
{
    public void OnClick(string path)
    {
        GameManager.Instance.SoundsManager.Request(path);
    }

    public void OnClick(int id)
    {
        GameManager.Instance.SoundsManager.Request(id);
    }
}
