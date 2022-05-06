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

    const float WaitSecond = 0.5f;

    bool _isOpenGame;
    bool _isOpenOption;
    bool _isOpenRanking;

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

        _isOpenGame = false;
        _isOpenOption = false;
        _isOpenRanking = false;
    }

    void OpenGame()
    {
        if (!_isOpenGame)
        {
            _isOpenGame = true;
            _isOpenOption = false;
            _isOpenRanking = false;
        }
        else _isOpenGame = false;

        BaseUI.Instance.CallBack("Title", "StartPanel", new object[] { _isOpenGame });
        BaseUI.Instance.CallBack("Title", "OptionPanel", new object[] { false });
        BaseUI.Instance.CallBack("Title", "RankingPanel", new object[] { false });
    }

    void OpenOption()
    {
        if (!_isOpenOption)
        {
            _isOpenOption = true;
            _isOpenGame = false;
            _isOpenRanking = false;
        }
        else _isOpenOption = false;

        BaseUI.Instance.CallBack("Title", "StartPanel", new object[] { false });
        BaseUI.Instance.CallBack("Title", "OptionPanel", new object[] { _isOpenOption });
        BaseUI.Instance.CallBack("Title", "RankingPanel", new object[] { false });
    }

    void OpenRanking()
    {
        if (!_isOpenRanking) 
        {
            _isOpenRanking = true;
            _isOpenGame = false;
            _isOpenOption = false;
        }
        else _isOpenRanking = false;

        BaseUI.Instance.CallBack("Title", "StartPanel", new object[] { false });
        BaseUI.Instance.CallBack("Title", "OptionPanel", new object[] { false });
        BaseUI.Instance.CallBack("Title", "RankingPanel", new object[] { _isOpenRanking });
    }
}
