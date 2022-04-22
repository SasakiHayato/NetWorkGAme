using UnityEngine;
// Photon �p�̖��O��Ԃ��Q�Ƃ���
using ExitGames.Client.Photon;

/// <summary>
/// �V�[���Đ�����SetUp�N���X
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
