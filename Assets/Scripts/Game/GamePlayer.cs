using UnityEngine;

/// <summary>
/// Playerを制御するクラス
/// </summary>

public class GamePlayer : MonoBehaviour
{
    TouchInputter _inputter = new TouchInputter();

    void Start()
    {
        _inputter.SetUp();
        _inputter.AddPushEvent(() => Click());
    }

    void Update()
    {
        _inputter.UpDate();
    }

    void Click()
    {
        GameManager.Instance.FieldManager.JudgeNotes();
    }
}
