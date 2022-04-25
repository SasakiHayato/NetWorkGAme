using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// Žg—pItem‚Ì•\Ž¦ 
/// </summary>

public class ItemLog : ChildrenUI
{
    [SerializeField] Vector2 _offsetPosition;
    [SerializeField] Vector2 _endPosition;

    Text _logTxt;
    RectTransform _rect;

    const float Duration = 2f;

    public override void SetUp()
    {
        _logTxt = GetComponent<Text>();
        _logTxt.text = "";

        _rect = GetComponent<RectTransform>();
    }

    public override void CallBack(object[] datas = null)
    {
        _logTxt.text = (string)datas[0];
        _rect.anchoredPosition = _offsetPosition;

        _rect.DOAnchorPos(_endPosition, Duration)
            .SetEase(Ease.Linear);
    }
}
