using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using System.Threading;
using Cysharp.Threading.Tasks;

/// <summary>
/// フェードの管理クラス
/// </summary>

public class Fader
{
    public enum FadeType
    {
        In,
        Out,
    }

    public enum ActionTiming
    {
        After,
        Center,
    }

    bool _isFade;

    readonly Vector2 Resolution = new Vector2(1600, 1000);
    const float DurationTime = 1f;

    Image _fadeImage;
    RectTransform _rect;

    Action _action;

    public Fader()
    {
        _isFade = false;
        _action = null;

        Create();
    }
    /// <summary>
    /// フェードのするためのPanelの生成
    /// </summary>
    void Create()
    {
        // Canvasの生成
        GameObject canvasObj = new GameObject("FadeCanvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999;

        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = Resolution;

        // Imageの生成
        GameObject imageObj = new GameObject("FadeImage");
        imageObj.transform.SetParent(canvasObj.transform);

        _fadeImage = imageObj.AddComponent<Image>();
        _fadeImage.color = Color.black;

        _rect = imageObj.GetComponent<RectTransform>();
        _rect.anchorMin = Vector2.zero;
        _rect.anchorMax = Vector2.one;
        _rect.offsetMin = Vector2.zero;
        _rect.offsetMax = Vector2.zero;
    }

    public Fader SetFade(FadeType type, Action action = null)
    {
        action += EndFade;

        _isFade = false;
        Color color = _fadeImage.color;

        if (type == FadeType.In)
        {
            color.a = 1;
            _fadeImage.color = color;
            _fadeImage.DOFade(0, DurationTime)
                .OnComplete(() => action?.Invoke());
        }
        else
        {
            color.a = 0;
            _fadeImage.color = color;
            _fadeImage.DOFade(1, DurationTime)
            .OnComplete(() => action?.Invoke());
        }

        return this;
    }

    public Fader Slide(Action action = null, ActionTiming timing = ActionTiming.After)
    {
        EndFadeEvent().Forget();
        
        _isFade = false;

        Color color = _fadeImage.color;
        color.a = 1;
        _fadeImage.color = color;

        Vector2 offSetPos = Resolution;
        offSetPos.x *= -1;
        offSetPos.y = 0;
        _rect.anchoredPosition = offSetPos;

        Sequence sequence = DOTween.Sequence();
        sequence.Append
            (
                _rect.DOAnchorPosX(0, DurationTime)
                .SetEase(Ease.OutCubic)
                .OnComplete(() => 
                {
                    if (timing == ActionTiming.Center) action?.Invoke();
                })
            ).Append
            (
                _rect.DOAnchorPosX(Resolution.x, DurationTime)
                .SetEase(Ease.OutCubic)
                .OnComplete(() => 
                {
                    if (timing == ActionTiming.After) action?.Invoke();
                    EndFade();
                })
            );

        return this;
    }

    public Fader AddEndFadeEvent(Action action)
    {
        _action += action;
        return this;
    }

    async UniTask EndFadeEvent()
    {
        await UniTask.WaitUntil(() => _isFade);
        _action?.Invoke();
        _action = null;
    }

    void EndFade() => _isFade = true;
}
