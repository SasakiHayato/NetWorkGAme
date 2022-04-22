using UnityEngine.UI;

/// <summary>
/// ƒRƒ“ƒ{”‚Ì•\¦
/// </summary>

public class ComboDisplay : ChildrenUI
{
    Text _comboText;

    const string Display = " Combo!!";

    public override void SetUp()
    {
        _comboText = GetComponent<Text>();

        _comboText.text = "0" + Display; 
    }

    public override void CallBack(object[] datas = null)
    {
        int combo = (int)datas[0];
        _comboText.text = combo.ToString() + Display;
    }
}
