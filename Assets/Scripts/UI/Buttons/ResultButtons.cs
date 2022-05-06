using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
// Photon 用の名前空間を参照する
using ExitGames.Client.Photon;

/// <summary>
/// Resultでのボタン制御
/// </summary>

public class ResultButtons : MonoBehaviour
{
    [SerializeField] Button _titleButton;

    const float WaitSeconds = 1f;

    void Start()
    {
        _titleButton.OnClickAsObservable()
            .ThrottleFirst(TimeSpan.FromSeconds(WaitSeconds))
            .TakeUntilDestroy(_titleButton)
            .Subscribe(_ => Title())
            .AddTo(_titleButton);
    }

    void Title()
    {
        EventData eventData = new EventData();
        eventData.Code = (byte)GameSate.Title;
        GameManager.Instance.OnEvent(eventData);
    }
}
