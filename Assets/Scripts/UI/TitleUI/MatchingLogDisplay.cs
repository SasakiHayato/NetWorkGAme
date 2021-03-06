using UnityEngine.UI;
// Photon 用の名前空間を参照する
using Photon.Pun;

public class MatchingLogDisplay : ChildrenUI
{
    Text _logTxt;

    const string FindText = "Find Player...";

    public override void SetUp()
    {
        _logTxt = GetComponent<Text>();
        _logTxt.text = FindText;
    }

    void Update()
    {
        if (PhotonNetwork.CurrentRoom == null) return;

        if (PhotonNetwork.CurrentRoom.IsOpen)
        {
            _logTxt.text = FindText;
        }
        else
        {
            _logTxt.text = "IsMacthing";
        }
    }

    public override void CallBack(object[] datas = null)
    {
        
    }
}
