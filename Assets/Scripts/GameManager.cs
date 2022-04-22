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
    public ScoreManager ScoreManager { get; private set; }
    public SoundsManager SoundsManager { get; private set; }

    public bool IsDebug { get; set; }

    public override void SetUp()
    {
        base.SetUp();

        CurrentGameState = GameSate.Title;
        FieldManager = null;
        ScoreManager = null;

        GameObject soundManager = Object.Instantiate((GameObject)Resources.Load("Systems/SoundsManager"));
        SoundsManager = soundManager.GetComponent<SoundsManager>();
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
        GameObject fieldManager = Object.Instantiate((GameObject)Resources.Load("Systems/FieldManager"));
        FieldManager = fieldManager.GetComponent<FieldManager>();

        GameObject scoreManager = Object.Instantiate((GameObject)Resources.Load("Systems/ScoreManager"));
        ScoreManager = scoreManager.GetComponent<ScoreManager>();
    }

    void GameEnd()
    {

    }
}
