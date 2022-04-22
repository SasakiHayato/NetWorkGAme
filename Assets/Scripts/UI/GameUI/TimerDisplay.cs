using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Œo‰ßŽžŠÔ‚Ì•\Ž¦
/// </summary>

public class TimerDisplay : ChildrenUI
{
    Text _timeTxt;

    const string Display = "Time : ";

    public override void SetUp()
    {
        _timeTxt = GetComponent<Text>();
        _timeTxt.text = Display + "00";
    }

    public override void CallBack(object[] datas = null)
    {
        int time = (int)datas[0];

        _timeTxt.text = Display + time.ToString("00");
    }
}
