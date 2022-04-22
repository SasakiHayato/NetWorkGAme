using UnityEngine;

/// <summary>
/// Player�𐧌䂷��N���X
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
        if (GameManager.Instance.CurrentGameState != GameSate.InGame) return;

        GameManager.Instance.SoundsManager.Request("PlayerTap");
        GameManager.Instance.FieldManager.JudgeNotes();
    }
}
