using UnityEngine;
// Photon 用の名前空間を参照する
using ExitGames.Client.Photon;
using Photon.Realtime;

public enum GameSate : byte
{
    Start,
    InGame,
    End,
    Title,
}

/// <summary>
/// ゲーム全体を管理するクラス
/// </summary>

public class GameManager : SingletonAttribute<GameManager>, IOnEventCallback
{
    Fader _fader;

    public GameSate CurrentGameState { get; private set; }
    public void SetGameState(GameSate gameSate) => CurrentGameState = gameSate;

    public FieldManager FieldManager { get; private set; }
    public ScoreManager ScoreManager { get; private set; }
    public SoundsManager SoundsManager { get; private set; }
    public NetworkManager NetworkManager { get; private set; }
    public GamePresenter GamePresenter { get; private set; }

    public bool IsDebug { get; set; }

    public override void SetUp()
    {
        base.SetUp();

        CurrentGameState = GameSate.Title;
        FieldManager = null;
        ScoreManager = null;

        GameObject soundManager = Object.Instantiate((GameObject)Resources.Load("Systems/SoundsManager"));
        SoundsManager = soundManager.GetComponent<SoundsManager>();

        _fader = new Fader();
        _fader.SetFade(Fader.FadeType.In);

        NetworkManager = Object.FindObjectOfType<NetworkManager>();
        GamePresenter = Object.FindObjectOfType<GamePresenter>();
    }

    public void OnEvent(EventData eventData)
    {
        CurrentGameState = (GameSate)eventData.Code;

        switch (eventData.Code)
        {
            case (byte)GameSate.Start:
                BaseUI.Instance.ParentActive("Game", true);
                BaseUI.Instance.ParentActive("Title", false);
                _fader.SetFade(Fader.FadeType.In, GamePresenter.CountDown);

                break;
            case (byte)GameSate.InGame:
                InGameSetUp();

                break;

            case (byte)GameSate.End:
                GameEnd();

                break;

            case (byte)GameSate.Title:

                break;
        }
    }

    void InGameSetUp()
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
