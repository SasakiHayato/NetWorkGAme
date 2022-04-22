using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class OptionSlider : ChildrenUI
{
    [SerializeField] Slider _bgmSlider;
    [SerializeField] Slider _seSlider;

    public override void SetUp()
    {
        _bgmSlider.ObserveEveryValueChanged(x => x.value)
            .TakeUntilDestroy(_bgmSlider)
            .Subscribe(_ => GameManager.Instance.SoundsManager.BGMVolume = _bgmSlider.value);

        _seSlider.ObserveEveryValueChanged(s => s.value)
            .Subscribe(_ => GameManager.Instance.SoundsManager.SEVolume = _seSlider.value);
    }

    public override void CallBack(object[] datas = null)
    {
        
    }
}
