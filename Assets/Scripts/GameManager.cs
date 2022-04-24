using UnityEngine;
using System.Collections.Generic;
// Photon 用の名前空間を参照する
using ExitGames.Client.Photon;
using Photon.Realtime;

public enum GameSate : byte
{
    Start,
    InGame,
    End,
    Result,
    Title,
}

public interface IManager
{
    Object ManagerObject();
    string ManagerPath();
}

/// <summary>
/// ゲーム全体を管理するクラス
/// </summary>

public class GameManager : SingletonAttribute<GameManager>, IOnEventCallback
{
    List<IManager> _iManagerList;
    Fader _fader;

    public GameSate CurrentGameState { get; private set; }
    public void SetGameState(GameSate gameSate) => CurrentGameState = gameSate;

    public FieldManager FieldManager { get; private set; }
    public ScoreManager ScoreManager { get; private set; }
    public SoundsManager SoundsManager { get; private set; }
    public NetworkManager NetworkManager { get; private set; }
    public GamePresenter GamePresenter { get; private set; }
    public ResultData ResultData { get; private set; }

    public bool IsDebug { get; set; }

    public override void SetUp()
    {
        base.SetUp();

        CurrentGameState = GameSate.Title;
        FieldManager = null;
        ScoreManager = null;

        GameObject soundManager = Object.Instantiate((GameObject)Resources.Load("Systems/SoundsManager"));
        SoundsManager = soundManager.GetComponent<SoundsManager>();
        SoundsManager.SetUp();

        _fader = new Fader();

        NetworkManager = Object.FindObjectOfType<NetworkManager>();
        GamePresenter = Object.FindObjectOfType<GamePresenter>();

        _iManagerList = new List<IManager>();
    }

    public void OnEvent(EventData eventData)
    {
        CurrentGameState = (GameSate)eventData.Code;
        
        switch (eventData.Code)
        {
            case (byte)GameSate.Start:
                SoundsManager.StopBGM();

                _fader.Slide(() => BaseUI.Instance.AtParantActive("Game"), Fader.ActionTiming.Center)
                    .AddEndFadeEvent(GamePresenter.CountDown);
                
                break;
            case (byte)GameSate.InGame:
                SoundsManager.Request("InGameBGM");
                SoundsManager.Request("CountDownStart");
                InGameSetUp();

                break;

            case (byte)GameSate.End:
                BaseUI.Instance.CallBack("Game", "EndGameButton", new object[] { false });
                GameEnd();

                break;
            case (byte)GameSate.Result:
                _fader.Slide(() => BaseUI.Instance.AtParantActive("Result"), Fader.ActionTiming.Center)
                    .AddEndFadeEvent(ResultData.SetData);

                BaseUI.Instance.CallBackParent("Title");
                BaseUI.Instance.CallBackParent("Game");

                break;

            case (byte)GameSate.Title:
                BaseUI.Instance.AtParantActive("Title");
                SoundsManager.Request("TitleBGM");
                RemoveManager("ResultData");

                break;
        }
    }

    void InGameSetUp()
    {
        GameObject fieldManager = Object.Instantiate((GameObject)Resources.Load("Systems/FieldManager"));
        FieldManager = fieldManager.GetComponent<FieldManager>();

        _iManagerList.Add(FieldManager);

        GameObject scoreManager = Object.Instantiate((GameObject)Resources.Load("Systems/ScoreManager"));
        ScoreManager = scoreManager.GetComponent<ScoreManager>();

        _iManagerList.Add(ScoreManager);
    }

    void GameEnd()
    {
        GameObject resultData = Object.Instantiate((GameObject)Resources.Load("Systems/ResultData"));
        ResultData = resultData.GetComponent<ResultData>();
        _iManagerList.Add(ResultData);

        RemoveManager("FieldManager");
        RemoveManager("ScoreManager");
    }

    void RemoveManager(string path)
    {
        IManager manager = _iManagerList.Find(m => m.ManagerPath() == path);

        if (manager != null)
        {
            _iManagerList.Remove(manager);
            Object.Destroy(manager.ManagerObject());
        }
    }
}
