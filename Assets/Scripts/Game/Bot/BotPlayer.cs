using UnityEngine;

/// <summary>
/// Soroゲームの際のBot制御クラス
/// </summary>

public class BotPlayer
{
    public void BotUpdate()
    {
        if (GameManager.Instance.CurrentGameState != GameSate.InGame) return;


    }

    void NotesJudge()
    {

    }
}
