
/// <summary>
/// InGameÇÃUIä«óùÉNÉâÉX
/// </summary>

public class GameUI : ParentUI
{
    public override void SetUp()
    {
        base.SetUp();

        if (GameManager.Instance.CurrentGameState != GameSate.InGame)
        {
            Active(false);
        }
    }

    public override void CallBack(object[] datas)
    {
        BaseUI.Instance.CallBack(Path, "Score", new object[] { 0 });
        BaseUI.Instance.CallBack(Path, "Combo", new object[] { 0 });
        BaseUI.Instance.CallBack(Path, "Timer", new object[] { 0 });
        BaseUI.Instance.CallBack(Path, "EndGameButton", new object[] { true });
    }
}
