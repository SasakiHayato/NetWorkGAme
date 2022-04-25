using UnityEngine;
using UnityEngine.UI;
using System;
using UniRx;
// Photon —p‚Ì–¼‘O‹óŠÔ‚ğQÆ‚·‚é
using ExitGames.Client.Photon;

/// <summary>
/// Title_StartButton‚Ì§Œä
/// </summary>

public class StartButtons : MonoBehaviour
{
    [SerializeField] Button _soroButton;
    [SerializeField] Button _multiButton;

    const float WaitSecounds = 1f;

    void Start()
    {
        _soroButton.OnClickAsObservable()
            .ThrottleFirst(TimeSpan.FromSeconds(WaitSecounds))
            .TakeUntilDestroy(_soroButton)
            .Subscribe(_ => Soro())
            .AddTo(_soroButton);

        _multiButton.OnClickAsObservable()
            .ThrottleFirst(TimeSpan.FromSeconds(WaitSecounds))
            .TakeUntilDestroy(_multiButton)
            .Subscribe(_ => Multi())
            .AddTo(_multiButton);
    }

    void Soro()
    {
        GameManager.Instance.SetGameType(GameType.Solo);

        EventData eventData = new EventData();
        eventData.Code = (byte)GameSate.Start;
        GameManager.Instance.OnEvent(eventData);
    }

    void Multi()
    {
        GameManager.Instance.SetGameType(GameType.Multi);
        GameManager.Instance.NetworkManager.Connect();
        BaseUI.Instance.ParentActive("Matching", true);
    }
}
