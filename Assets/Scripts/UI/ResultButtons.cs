using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
// Photon �p�̖��O��Ԃ��Q�Ƃ���
using ExitGames.Client.Photon;

/// <summary>
/// Result�ł̃{�^������
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
