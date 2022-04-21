using UnityEngine;
// Photon 用の名前空間を参照する
using ExitGames.Client.Photon;
using Photon.Realtime;

public enum GameSate : byte
{
    Start,
    End,
    Title,
}

/// <summary>
/// ゲーム全体を管理するクラス
/// </summary>

public class GameManager : SingletonAttribute<GameManager>, IOnEventCallback
{
    public GameSate CurrentGameState { get; private set; }
    public void SetGameState(GameSate gameSate) => CurrentGameState = gameSate;

    public FieldManager FieldManager { get; private set; }

    public override void SetUp()
    {
        base.SetUp();

        CurrentGameState = GameSate.Title;
        FieldManager = null;
    }

    public void OnEvent(EventData eventData)
    {
        switch (eventData.Code)
        {
            case (byte)GameSate.Start:
                GameSetUp();

                break;
            case (byte)GameSate.End:
                GameEnd();

                break;

            case (byte)GameSate.Title:

                break;
        }
    }

    void GameSetUp()
    {
        GameObject obj = Object.Instantiate((GameObject)Resources.Load("Systems/FieldManager"));
        FieldManager = obj.GetComponent<FieldManager>();
    }

    void GameEnd()
    {

    }
}
