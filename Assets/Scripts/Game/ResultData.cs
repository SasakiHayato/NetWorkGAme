using UnityEngine;

/// <summary>
/// UnGame終了時のData管理クラス
/// </summary>

public class ResultData : MonoBehaviour, IManager
{
    int _score;
    int _comboCount;

    public void SetData()
    {
        _score = GameManager.Instance.ScoreManager.CurrentScore;
        _comboCount = GameManager.Instance.ScoreManager.CurrentComboCount;

        object[] data = { _score, _comboCount };
        BaseUI.Instance.CallBack("Result", "ResultDisplay", data);
        BaseUI.Instance.CallBack("Result", "ResultAnimation");
    }

    // IManager
    public Object ManagerObject() => gameObject;
    public string ManagerPath() => GetType().Name;
}
