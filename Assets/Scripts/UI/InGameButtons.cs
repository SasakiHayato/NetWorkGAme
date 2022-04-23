using UnityEngine;
using UnityEngine.UI;
using System;
using UniRx;

/// <summary>
/// InGame“à‚ÌButton‚ÌŠÇ—ƒNƒ‰ƒX
/// </summary>

public class InGameButtons : MonoBehaviour
{
    [SerializeField] Button _endGameButtons;

    void Start()
    {
        _endGameButtons.OnClickAsObservable()
            .ThrottleFirst(TimeSpan.FromSeconds(1f))
            .TakeUntilDestroy(_endGameButtons)
            .Subscribe(_ => EndGame());
    }

    void EndGame()
    {

    }
}
