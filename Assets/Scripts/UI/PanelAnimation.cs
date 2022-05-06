using UnityEngine;
using DG.Tweening;

/// <summary>
/// Panelのアニメーション制御
/// </summary>

public class PanelAnimation : ChildrenUI
{
    enum AnimType
    {
        Move,
        Scale,
    }

    [SerializeField] AnimType _animType = AnimType.Move;
    [SerializeField] Vector2 _offSet;
    [SerializeField] Vector2 _set;
    
    [SerializeField] Ease _ease = Ease.Linear;

    RectTransform _rect;

    const float DurationTime = 0.2f;

    public override void SetUp()
    {
        _rect = GetComponent<RectTransform>();
        _rect.anchoredPosition = _offSet;
    }

    public override void CallBack(object[] datas = null)
    {
        bool isOpen = (bool)datas[0];

        if (isOpen)
        {
            if (_animType == AnimType.Move)
            {
                _rect.DOAnchorPos(_set, DurationTime)
                .SetEase(_ease);
            }
            else
            {
                _rect.DOScale(_set, DurationTime)
                    .SetEase(_ease);
            }
        }
        else
        {
            if (_animType == AnimType.Move)
            {
                _rect.DOAnchorPos(_offSet, DurationTime)
                .SetEase(_ease);
            }
            else
            {
                _rect.DOScale(_offSet, DurationTime)
                    .SetEase(_ease);
            }
        }
    }
}
