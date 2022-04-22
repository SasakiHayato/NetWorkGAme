using UnityEngine;
using DG.Tweening;

/// <summary>
/// TitleStartPanelのアニメーション制御
/// </summary>

public class TitleStartPanel : ChildrenUI
{
    [SerializeField] Vector2 _offSetPotision;
    [SerializeField] Vector2 _setPositision;

    RectTransform _rect;

    const float DurationTime = 0.2f;

    public override void SetUp()
    {
        _rect = GetComponent<RectTransform>();
        _rect.anchoredPosition = _offSetPotision;
    }

    public override void CallBack(object[] datas = null)
    {
        bool isOpen = (bool)datas[0];

        if (isOpen)
        {
            _rect.DOAnchorPos(_setPositision, DurationTime)
                .SetEase(Ease.Linear);
        }
        else
        {
            _rect.DOAnchorPos(_offSetPotision, DurationTime)
                .SetEase(Ease.Linear);
        }
    }
}
