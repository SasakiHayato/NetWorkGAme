
/// <summary>
/// ItemUI�̊Ǘ��N���X
/// </summary>

public class ItemUI : ParentUI
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
        
    }
}
