using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
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

    Action _fadeEndAction;
    Action _fadeCenterAction;

    public Fader()
    {
        _isFade = false;
        _fadeEndAction = null;
        _fadeCenterAction = null;

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

    public Fader SetImageSouce(Sprite sprite = null)
    {
        if (sprite == null)
        {
            _fadeImage.color = Color.black;
        }
        else
        {
            _fadeImage.color = Color.white;
            _fadeImage.sprite = sprite;
        }

        return this;
    }

    public Fader SetFade(FadeType type, Action action = null)
    {
        _fadeImage.enabled = true;
        action += EndFade;
        CenterFadeEvent().Forget();

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
        _fadeImage.enabled = true;
        EndFadeEvent().Forget();
        CenterFadeEvent().Forget();
        
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

    public Fader AddCenterFadeEvent(Action action)
    {
        _fadeCenterAction += action;
        return this;
    }

    async UniTask CenterFadeEvent()
    {
        float waitTime = DurationTime / 2;
        await UniTask.Delay(TimeSpan.FromSeconds(waitTime));

        _fadeCenterAction?.Invoke();
        _fadeCenterAction = null;
    }

    public Fader AddEndFadeEvent(Action action)
    {
        _fadeEndAction += action;
        return this;
    }

    async UniTask EndFadeEvent()
    {
        await UniTask.WaitUntil(() => _isFade);
        _fadeEndAction?.Invoke();
        _fadeEndAction = null;
    }

    void EndFade()
    {
        _fadeImage.enabled = false;
        _isFade = true;
    }
}
