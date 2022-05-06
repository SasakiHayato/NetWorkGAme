using UnityEngine.UI;

public class RankingLog : ChildrenUI
{
    Text _txt;

    public override void SetUp()
    {
        _txt = GetComponent<Text>();
    }

    public override void CallBack(object[] datas = null)
    {
        throw new System.NotImplementedException();
    }
}
