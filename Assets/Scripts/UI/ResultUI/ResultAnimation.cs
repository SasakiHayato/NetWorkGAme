using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;

/// <summary>
/// ResultÇÃAnimationêßå‰
/// </summary>

public class ResultAnimation : ChildrenUI
{
    [SerializeField] List<AnimData> _animDatas;

    [System.Serializable]
    class AnimData
    {
        public RectTransform RectTransform;
        public Vector2 OffSetPosition;
        public Vector2 SetPosition;
    }

    bool _isEndAnim;
    int _animID;

    const float Duration = 0.2f;

    public override void SetUp()
    {
        _animDatas.ForEach(d => d.RectTransform.anchoredPosition = d.OffSetPosition);
        _isEndAnim = false;
    }

    public override void CallBack(object[] datas = null)
    {
        bool isInit = (bool)datas[0];

        if (isInit) _animDatas.ForEach(d => d.RectTransform.anchoredPosition = d.OffSetPosition);
        else SetAnim();
    }

    void SetAnim()
    {
        if (_animID >= _animDatas.Count) return;
        else _isEndAnim = false;

        WaitAnim().Forget();

        AnimData animData = _animDatas[_animID];
        RectTransform rect = animData.RectTransform;

        rect.DOAnchorPos(animData.SetPosition, Duration)
            .SetEase(Ease.Linear)
            .OnComplete(() => _isEndAnim = true);
    }

    async UniTask WaitAnim()
    {
        await UniTask.WaitUntil(() => _isEndAnim);
        _animID++;
        SetAnim();
    }
}
