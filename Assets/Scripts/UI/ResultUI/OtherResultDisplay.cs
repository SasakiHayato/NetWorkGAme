using UnityEngine.UI;

/// <summary>
/// ‘¼ŽÒ‚ÌScore•\Ž¦
/// </summary>

public class OtherResultDisplay : ChildrenUI
{
    Text _scoreTxt;

    const string ScoreDisplay = "Score : ";

    public override void SetUp()
    {
        _scoreTxt = GetComponent<Text>();
        _scoreTxt.text = ScoreDisplay + "000000";
    }

    public override void CallBack(object[] datas = null)
    {
        int score = (int)datas[0];
        _scoreTxt.text = ScoreDisplay + score.ToString("000000");
    }
}
