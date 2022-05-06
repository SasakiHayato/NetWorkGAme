using UnityEngine.UI;

/// <summary>
/// Entry‚ÌŒx‚Ì•\¦
/// </summary>

public class WarningLog : ChildrenUI
{
    Text _txt;

    public override void SetUp()
    {
        _txt = GetComponent<Text>();
        _txt.enabled = false;
    }

    public override void CallBack(object[] datas = null)
    {
        _txt.enabled = true;
    }
}
