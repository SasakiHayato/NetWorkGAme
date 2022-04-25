using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Result‚ÌŒ‹‰Ê•\Ž¦
/// </summary>

public class MyResultDisplay : ChildrenUI
{
    [SerializeField] Text _scoreText;
    [SerializeField] Text _maxComboText;
    [SerializeField] Text _comboCountText;

    const string ScoreDisplay = "Score : ";
    const string MaxComboDisplay = "MaxCombo :";
    const string ComboCountDisplay = "Combo : ";

    public override void SetUp()
    {
        _scoreText.text = ScoreDisplay + "000000";
        _maxComboText.text = MaxComboDisplay + "00";
        _comboCountText.text = ComboCountDisplay + "00";
    }

    public override void CallBack(object[] datas = null)
    {
        int score = (int)datas[0];
        int comboCount = (int)datas[1];
        int maxCombo = (int)datas[2];

        _scoreText.text = ScoreDisplay + score.ToString("000000");
        _maxComboText.text = MaxComboDisplay + maxCombo.ToString("00");
        _comboCountText.text = ComboCountDisplay + comboCount.ToString("00");
    }
}
