using UnityEngine;
// Photon 用の名前空間を参照する
using ExitGames.Client.Photon;

/// <summary>
/// シーン再生時のSetUpクラス
/// </summary>

public class GamePresenter : MonoBehaviour
{
    [SerializeField] GameSate _gameSate;
    [SerializeField] bool _isDebug;

    void Awake()
    {
        GameManager gameManager = new GameManager();
        GameManager.SetInstance(gameManager, gameManager);

        GameManager.Instance.IsDebug = _isDebug;
    }

    void Start()
    {
        if (_isDebug) IsDebug();

        BaseUI baseUI = new BaseUI();
        BaseUI.SetInstance(baseUI, baseUI);
    }

    void IsDebug()
    {
        GameManager.Instance.SetGameState(_gameSate);
        EventData eventData = new EventData();
        eventData.Code = (byte)GameManager.Instance.CurrentGameState;
        GameManager.Instance.OnEvent(eventData);
    }
}
