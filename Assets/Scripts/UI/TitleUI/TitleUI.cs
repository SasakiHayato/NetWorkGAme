
/// <summary>
/// Title��UI�Ǘ��N���X
/// </summary>

public class TitleUI : ParentUI
{
    public override void SetUp()
    {
        base.SetUp();

        if (GameManager.Instance.CurrentGameState != GameSate.Title)
        {
            Active(false);
        }
    }

    public override void CallBack(object[] datas)
    {
        
    }
}
