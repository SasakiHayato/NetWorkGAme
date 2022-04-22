using UnityEngine;
using UnityEngine.UI;
using System;
using UniRx;

/// <summary>
/// TitleButtonÇÃêßå‰
/// </summary>

public class TitleButtons : MonoBehaviour
{
    [SerializeField] Button _gameButton;
    [SerializeField] Button _optionButton;
    [SerializeField] Button _rankingButton;

    const float WaitSecond = 1f;

    void Start()
    {
        _gameButton.OnClickAsObservable()
            .ThrottleFirst(TimeSpan.FromSeconds(WaitSecond))
            .TakeUntilDestroy(_gameButton)
            .Subscribe(_ => OpenGame())
            .AddTo(_gameButton);

        _optionButton.OnClickAsObservable()
            .ThrottleFirst(TimeSpan.FromSeconds(WaitSecond))
            .TakeUntilDestroy(_optionButton)
            .Subscribe(_ => OpenOption())
            .AddTo(_optionButton);

        _rankingButton.OnClickAsObservable()
            .ThrottleFirst(TimeSpan.FromSeconds(WaitSecond))
            .TakeUntilDestroy(_rankingButton)
            .Subscribe(_ => OpenRanking())
            .AddTo(_rankingButton);
    }

    void OpenGame()
    {
        BaseUI.Instance.CallBack("Title", "StartPanel", new object[] { true });
        BaseUI.Instance.CallBack("Title", "OptionPanel", new object[] { false });
    }

    void OpenOption()
    {
        BaseUI.Instance.CallBack("Title", "StartPanel", new object[] { false });
        BaseUI.Instance.CallBack("Title", "OptionPanel", new object[] { true });
    }

    void OpenRanking()
    {

    }
}
