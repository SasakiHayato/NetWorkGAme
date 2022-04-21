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
        _scoreTxt.text = Display;
    }

    public override void CallBack(object[] datas = null)
    {
        _scoreTxt.text = Display + datas[0].ToString();
    }
}
