
/// <summary>
/// �Q�[���I�����UI�Ǘ��N���X
/// </summary>

public class ResultUI : ParentUI
{
    public override void SetUp()
    {
        base.SetUp();

        if (GameManager.Instance.CurrentGameState != GameSate.End)
        {
            Active(false);
        }
    }

    public override void CallBack(object[] datas = null)
    {
        
    }
}
