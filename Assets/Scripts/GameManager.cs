using UnityEngine;
using System.Collections.Generic;
// Photon 用の名前空間を参照する
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

public enum GameSate : byte
{
    Start,
    InGame,
    End,
    Result,
    Title,
}

public enum GameType
{
    Solo,
    Multi,
}

public interface IManager
{
    GameObject ManagerObject();
    string ManagerPath();
}

/// <summary>
/// ゲーム全体を管理するクラス
/// </summary>

public class GameManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    private static GameManager s_instance = null;
    public static GameManager Instance => s_instance;

    List<IManager> _iManagerList;
    Fader _fader;

    public GameSate CurrentGameState { get; private set; }
    public void SetGameState(GameSate gameSate) => CurrentGameState = gameSate;

    public GameType CurrentGameType { get; private set; }
    public void SetGameType(GameType gameType) => CurrentGameType = gameType;

    public FieldManager FieldManager { get; private set; }
    public ScoreManager ScoreManager { get; private set; }
    public SoundsManager SoundsManager { get; private set; }
    public NetworkManager NetworkManager { get; private set; }
    public GamePresenter GamePresenter { get; private set; }
    public ResultData ResultData { get; private set; }
    public ItemManager ItemManager { get; private set; }
    public BotManager BotManager { get; private set; }

    public bool IsDebug { get; set; }
    public bool IsUsingBot { get; private set; }

    int _viewID = 2;

    void Awake()
    {
        if (s_instance == null) s_instance = this;

        CurrentGameState = GameSate.Title;
        FieldManager = null;
        ScoreManager = null;

        GameObject soundManager = Instantiate((GameObject)Resources.Load("Systems/SoundsManager"));
        SoundsManager = soundManager.GetComponent<SoundsManager>();
        SoundsManager.SetUp();

        _fader = new Fader();

        NetworkManager = FindObjectOfType<NetworkManager>();
        GamePresenter = FindObjectOfType<GamePresenter>();

        _iManagerList = new List<IManager>();
    }

    public void Opning()
    {
        _fader.SetFade(Fader.FadeType.In, () => _fader.SetImageSouce(GamePresenter.FadeSprite));

        GamePresenter.ChangeBackGround(CurrentGameState);
        SoundsManager.Request("TitleBGM");
    }

    public void OnEvent(EventData eventData)
    {
        CurrentGameState = (GameSate)eventData.Code;
        
        switch (eventData.Code)
        {
            case (byte)GameSate.Start:
                SoundsManager.StopBGM();

                if (CurrentGameType == GameType.Solo)
                {
                    IsUsingBot = true;

                    GameObject botManager = Instantiate((GameObject)Resources.Load("Systems/BotManager"));
                    BotManager = botManager.GetComponent<BotManager>();

                    _iManagerList.Add(BotManager);
                }
                else
                {
                    IsUsingBot = false;
                }

                _fader.Slide(() => BaseUI.Instance.AtParantActive("Game"), Fader.ActionTiming.Center)
                    .AddCenterFadeEvent(() => GamePresenter.ChangeBackGround(CurrentGameState))
                    .AddCenterFadeEvent(() => GamePresenter.InGaneObject.SetActive(true))
                    .AddEndFadeEvent(GamePresenter.CountDown);
                
                break;
            case (byte)GameSate.InGame:
                BaseUI.Instance.ParentActive("Item", true);

                SoundsManager.Request("InGameBGM");
                SoundsManager.Request("CountDownStart");
                InGameSetUp();

                break;

            case (byte)GameSate.End:
                SoundsManager.StopBGM();
                BaseUI.Instance.CallBack("Game", "EndGameButton", new object[] { false });
                GameEnd();

                break;
            case (byte)GameSate.Result:
                _fader.Slide(() => BaseUI.Instance.AtParantActive("Result"), Fader.ActionTiming.Center)
                    .AddEndFadeEvent(() => BaseUI.Instance.CallBack("Result", "ResultAnimation", new object[] { false }))
                    .AddCenterFadeEvent(() => GamePresenter.ChangeBackGround(CurrentGameState))
                    .AddCenterFadeEvent(() => GamePresenter.InGaneObject.SetActive(false))
                    .AddEndFadeEvent(() => ResultData.Judge())
                    .AddEndFadeEvent(() => SoundsManager.Request("ResultBGM"));
                
                ResultData.SetMyData();

                if (CurrentGameType == GameType.Multi)
                {
                    ResultData.SendMyData();
                }

                BaseUI.Instance.CallBackParent("Title");
                BaseUI.Instance.CallBackParent("Game");

                break;

            case (byte)GameSate.Title:

                if (CurrentGameType == GameType.Multi)
                {
                    PhotonNetwork.Disconnect();
                }

                GamePresenter.ChangeBackGround(CurrentGameState);
                BaseUI.Instance.CallBackParent("Result");
                BaseUI.Instance.AtParantActive("Title");
                SoundsManager.Request("TitleBGM");
                RemoveManager("ResultData");

                break;
        }

        AddPhotonView();
    }

    void AddPhotonView()
    {
        if (CurrentGameType != GameType.Multi || _iManagerList.Count <= 0) return;

        foreach (IManager iManager in _iManagerList)
        {
            INetworkManager network = iManager.ManagerObject().GetComponent<INetworkManager>();
            if (network == null) continue;

            if (network.ManagerPhotonView == null)
            {
                network.ManagerPhotonView = iManager.ManagerObject().AddComponent<PhotonView>();
                network.ManagerPhotonView.ViewID = _viewID;

                _viewID++;
                if (_viewID > 900) _viewID = 0;
            }
        }
    }

    void InGameSetUp()
    {
        GameObject fieldManager = Instantiate((GameObject)Resources.Load("Systems/FieldManager"));
        FieldManager = fieldManager.GetComponent<FieldManager>();

        _iManagerList.Add(FieldManager);

        GameObject scoreManager = Instantiate((GameObject)Resources.Load("Systems/ScoreManager"));
        ScoreManager = scoreManager.GetComponent<ScoreManager>();

        _iManagerList.Add(ScoreManager);

        GameObject itemManager = Instantiate((GameObject)Resources.Load("Systems/ItemManager"));
        ItemManager = itemManager.GetComponent<ItemManager>();

        _iManagerList.Add(ItemManager);
    }

    void GameEnd()
    {
        GameObject resultData = Instantiate((GameObject)Resources.Load("Systems/ResultData"));
        ResultData = resultData.GetComponent<ResultData>();
        _iManagerList.Add(ResultData);

        RemoveManager("FieldManager");
        RemoveManager("ScoreManager");
        RemoveManager("ItemManager");
        RemoveManager("BotManager");
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
