
/// <summary>
/// InGameのUI管理クラス
/// </summary>

public class GameUI : ParentUI
{
    public override void SetUp()
    {
        base.SetUp();

        if (GameManager.Instance.CurrentGameState != GameSate.Start)
        {
            Active(false);
        }
    }

    public override void CallBack(object[] datas)
    {
        
    }
}
