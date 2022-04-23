using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// EndGameButtonのアニメーション制御
/// </summary>

public class EndGameButtonAnimation : ChildrenUI
{
    RectTransform _rect;
    Image _image;

    const float Duration = 0.5f;

    public override void SetUp()
    {
        _rect = GetComponent<RectTransform>();
        _rect.localScale = Vector2.zero;

        _image = GetComponent<Image>();
        _image.raycastTarget = false;
    }

    public override void CallBack(object[] datas = null)
    {
        _rect.DOScale(Vector3.one, Duration)
            .SetEase(Ease.Linear)
            .OnComplete(() => _image.raycastTarget = true);
    }
}
