using UnityEngine.UI;

/// <summary>
/// èüîsï\é¶
/// </summary>

public class ResultJudgeDisplay : ChildrenUI
{
    Text _judgeTxt;

    const string WinDisplay = "Win";
    const string LoseDidplay = "Lose";

    public override void SetUp()
    {
        _judgeTxt = GetComponent<Text>();
        _judgeTxt.text = "";
    }

    public override void CallBack(object[] datas = null)
    {
        ResultData.JudgeType type = (ResultData.JudgeType)datas[0];
        string judge = "";

        switch (type)
        {
            case ResultData.JudgeType.Win:
                judge = WinDisplay;

                break;
            case ResultData.JudgeType.Lose:
                judge = LoseDidplay;

                break;
            case ResultData.JudgeType.Dorrow:


                break;
        }

        _judgeTxt.text = judge;
    }
}
