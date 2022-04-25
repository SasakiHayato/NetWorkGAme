
/// <summary>
/// コンボ数の管理クラス
/// </summary>

public class ComboCounter 
{
    int _count;
    public int CurrentCount => _count;

    int _maxCount;
    public int MaxCount => _maxCount;

    int _comboEffectCount;
    int _addEffectCount;

    public ComboCounter(int effectCount)
    {
        _count = 0;
        _maxCount = int.MinValue;

        _addEffectCount = 0;
        _comboEffectCount = effectCount;
    }

    public void Check(ScoreType type)
    {
        if (type != ScoreType.Miss)
        {
            _count++;
            _addEffectCount++;
        }
        else
        {
            _count = 0;
            _addEffectCount = 0;
        }

        if (_maxCount < _count) _maxCount = _count;

        SetEffect();
        BaseUI.Instance.CallBack("Game", "Combo", new object[] { CurrentCount });
    }

    void SetEffect()
    {
        if (_comboEffectCount <= _addEffectCount)
        {
            _addEffectCount = 0;
            GameManager.Instance.SoundsManager.Request("Blessing");
            BaseUI.Instance.CallBack("Game", "Blessing");

        }
    }
}
