using UnityEngine;
using System.Collections;
// Photon 用の名前空間を参照する
using ExitGames.Client.Photon;

/// <summary>
/// シーン再生時のSetUpクラス
/// </summary>

public class GamePresenter : MonoBehaviour
{
    [SerializeField] GameSate _gameSate;
    [SerializeField] int _countDownTime;
    [SerializeField] bool _isDebug;

    void Awake()
    {
        GameManager gameManager = new GameManager();
        GameManager.SetInstance(gameManager, gameManager);

        GameManager.Instance.IsDebug = _isDebug;
    }

    void Start()
    {
        BaseUI baseUI = new BaseUI();
        BaseUI.SetInstance(baseUI, baseUI);

        if (_isDebug) IsDebug();
        else
        {
            GameManager.Instance.Opning();
        }
    }

    void IsDebug()
    {
        GameManager.Instance.SetGameState(_gameSate);
        EventData eventData = new EventData();
        eventData.Code = (byte)GameManager.Instance.CurrentGameState;
        GameManager.Instance.OnEvent(eventData);
    }

    /// <summary>
    /// ゲーム開始のカウントダウンをさせる
    /// </summary>
    public void CountDown()
    {
        StartCoroutine(ICounDown());
    }

    IEnumerator ICounDown()
    {
        bool endCount = false;
        float timer = 0;
        int saveTime = int.MinValue;

        while (!endCount)
        {
            timer += Time.deltaTime;
            if (timer > _countDownTime) endCount = true;
            else
            {
                if ((int)timer != saveTime)
                {
                    saveTime = (int)timer;
                    GameManager.Instance.SoundsManager.Request("CountDown");
                    string data = (_countDownTime - saveTime).ToString();
                    BaseUI.Instance.CallBack("Game", "CountDown", new object[] { data, false });
                }
            }

            yield return null;
        }

        EventData eventData = new EventData();
        eventData.Code = (byte)GameSate.InGame;
        GameManager.Instance.OnEvent(eventData);

        BaseUI.Instance.CallBack("Game", "CountDown", new object[] { "", true });
    }
}
