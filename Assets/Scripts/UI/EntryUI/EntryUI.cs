
/// <summary>
/// EntryUI‚ÌŠÇ—ƒNƒ‰ƒX
/// </summary>

public class EntryUI : ParentUI
{
    public override void SetUp()
    {
        base.SetUp();

        if (GameManager.Instance.CurrentGameState != GameSate.Result)
        {
            Active(false);
        }
    }

    public override void CallBack(object[] datas)
    {
        
    }
}
