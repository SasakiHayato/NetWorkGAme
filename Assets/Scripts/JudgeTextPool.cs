using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// JudgeDisplayÇÃï°êª
/// </summary>

public class JudgeTextPool : MonoBehaviour, IPool
{
    [SerializeField] Vector2 _offSetPosition;
    [SerializeField] Vector2 _setPosition;

    Text _judgeTxt;
    RectTransform _rect;

    public bool IsUse { get; private set; }

    const float Duration = 0.5f;

    public void SetUp(Transform parent)
    {
        _judgeTxt = GetComponent<Text>();
        _judgeTxt.text = "";

        _rect = GetComponent<RectTransform>();
        _rect.anchoredPosition = _offSetPosition;

        transform.SetParent(parent);
    }

    public void Use(string judge)
    {
        _judgeTxt.text = judge + "!!";
        IsUse = true;

        _rect.DOAnchorPos(_setPosition, Duration)
            .SetEase(Ease.Linear)
            .OnComplete(Delete);
    }

    public void Delete()
    {
        IsUse = false;
        _rect.anchoredPosition = _offSetPosition;
        _judgeTxt.text = "";
    }
}
