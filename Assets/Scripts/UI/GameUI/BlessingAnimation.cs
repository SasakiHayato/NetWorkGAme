using UnityEngine;
using DG.Tweening;

/// <summary>
/// InGameÇ≈ÇÃèjïüAnimationÇÃä«óù
/// </summary>

public class BlessingAnimation : ChildrenUI
{
    [SerializeField] GameObject _blessinPrefab;
    [SerializeField] Vector2 _offSetPosition;
    [SerializeField] Vector2 _setPosition;

    const float Duration = 1f;

    public override void SetUp()
    {
        _blessinPrefab.transform.position = _offSetPosition;
    }

    public override void CallBack(object[] datas = null)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(
            _blessinPrefab.transform.DOMove(_setPosition, Duration)
            .SetEase(Ease.OutElastic))
            .Append(
            _blessinPrefab.transform.DOMove(_offSetPosition, Duration)
            );
    }
}
