using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

/// <summary>
/// EntryButton‚ÌŠÇ—ƒNƒ‰ƒX
/// </summary>

public class EntryButtons : MonoBehaviour
{
    [SerializeField] Button _entryButton;

    const float WaitSeconds = 1f;

    void Start()
    {
        _entryButton.OnClickAsObservable()
            .TakeUntilDestroy(_entryButton)
            .ThrottleFirst(TimeSpan.FromSeconds(WaitSeconds))
            .Subscribe(_ => Entry());
    }

    void Entry()
    {
        GameManager.Instance.RankingManager.Entry();
    }
}
