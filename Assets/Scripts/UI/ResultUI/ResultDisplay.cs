using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Result‚ÌŒ‹‰Ê•\Ž¦
/// </summary>

public class ResultDisplay : ChildrenUI
{
    [SerializeField] Text _scoreText;
    [SerializeField] Text _comboCountText;

    const string ScoreDisplay = "Score : ";
    const string ComboCountDisplay = "Combo : ";

    public override void SetUp()
    {
        _scoreText.text = ScoreDisplay + "000000";
        _comboCountText.text = ComboCountDisplay + "00";
    }

    public override void CallBack(object[] datas = null)
    {
        int score = (int)datas[0];
        int comboCount = (int)datas[1];

        _scoreText.text = ScoreDisplay + score.ToString("000000");
        _comboCountText.text = ComboCountDisplay + comboCount.ToString("00");
    }
}
