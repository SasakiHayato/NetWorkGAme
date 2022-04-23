using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// カウントダウンの表示
/// </summary>

public class CountDownDisplay : ChildrenUI
{
    [SerializeField] Vector2 _setScale;
    [SerializeField] string _startText;

    Text _countDouwnTxt;
    RectTransform _rect;

    const float DurationTime = 0.4f;

    public override void SetUp()
    {
        _countDouwnTxt = GetComponent<Text>();
        _countDouwnTxt.text = "";

        _rect = GetComponent<RectTransform>();
    }

    public override void CallBack(object[] datas = null)
    {
        if ((string)datas[0] == "") datas[0] = _startText;

        _countDouwnTxt.text = (string)datas[0];
        bool isEnd = (bool)datas[1];

        Sequence sequence = DOTween.Sequence();
        _rect.localScale = Vector2.zero;

        sequence.Join(_rect.DOScale(_setScale, DurationTime))
            .Join(_rect.DOLocalRotate(new Vector3(0, 0, 360), DurationTime, RotateMode.FastBeyond360))
            .OnComplete(() => 
            {
                if (isEnd)
                {
                    Color color = _countDouwnTxt.color;

                    DOTween.To
                        (
                            () => color.a,
                            (x) =>
                            {
                                color.a = x;
                                _countDouwnTxt.color = color;
                            },
                            0,
                            DurationTime
                        )
                        .SetDelay(1f);
                }
            });
    }
}
