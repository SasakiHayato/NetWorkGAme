using UnityEngine;

/// <summary>
/// ƒm[ƒc”»’è‚Ì‰Â‹‰»
/// </summary>

public class JudgeDisplay : ChildrenUI
{
    [SerializeField] Transform _parent;
    [SerializeField] JudgeTextPool _judgeTextPool;
    
    ObjectPool<JudgeTextPool> _displayPool;

    public bool IsUse { get; private set; }

    public override void SetUp()
    {
        _displayPool = new ObjectPool<JudgeTextPool>();
        _displayPool.SetUp(_judgeTextPool, _parent, 10);

        GetComponent<UnityEngine.UI.Text>().text = "";
    }

    public override void CallBack(object[] datas = null)
    {
        _displayPool.Respons().Use((string)datas[0]);
    }
}
