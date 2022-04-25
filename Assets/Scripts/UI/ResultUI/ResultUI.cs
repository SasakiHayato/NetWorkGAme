
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
        object[] initData = { 0, 0, 0 };
        BaseUI.Instance.CallBack(Path, "MyResultDisplay", initData);
        BaseUI.Instance.CallBack(Path, "ResultAnimation", new object[] { true });
    }
}
