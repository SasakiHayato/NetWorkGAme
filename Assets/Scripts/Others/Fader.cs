using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

/// <summary>
/// �t�F�[�h�̊Ǘ��N���X
/// </summary>

public class Fader
{
    public enum FadeType
    {
        In,
        Out,
    }

    readonly Vector2 Resolution = new Vector2(1600, 1000);
    const float DurationTime = 1f;

    Image _fadeImage;

    public Fader()
    {
        Create();
    }
    /// <summary>
    /// �t�F�[�h�̂��邽�߂�Panel�̐���
    /// </summary>
    void Create()
    {
        // Canvas�̐���
        GameObject canvasObj = new GameObject("FadeCanvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999;

        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = Resolution;

        // Image�̐���
        GameObject imageObj = new GameObject("FadeImage");
        imageObj.transform.SetParent(canvasObj.transform);

        _fadeImage = imageObj.AddComponent<Image>();
        _fadeImage.color = Color.black;

        RectTransform rect = imageObj.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
    }

    public void SetFade(FadeType type, Action action = null)
    {
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
    }

    public void SetFade(Action action)
    {
        action.Invoke();
    }
}