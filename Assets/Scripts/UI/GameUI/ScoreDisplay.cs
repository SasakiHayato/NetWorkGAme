using UnityEngine.UI;

/// <summary>
/// ScoreÇÉQÅ[ÉÄè„Ç…ï\é¶Ç∑ÇÈ
/// </summary>

public class ScoreDisplay : ChildrenUI
{
    Text _scoreTxt;

    const string Display = "Score : ";

    public override void SetUp()
    {
        _scoreTxt = GetComponent<Text>();
        _scoreTxt.text = Display + "00000000000";
    }

    public override void CallBack(object[] datas = null)
    {
        int score = (int)datas[0];
        _scoreTxt.text = Display + score.ToString("00000000000");
    }
}
