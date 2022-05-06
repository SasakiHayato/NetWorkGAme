using UnityEngine;
using UnityEngine.UI;
using System;
using UniRx;
// Photon 用の名前空間を参照する
using ExitGames.Client.Photon;

/// <summary>
/// InGame内のButtonの管理クラス
/// </summary>

public class InGameButtons : MonoBehaviour
{
    [SerializeField] Button _endGameButtons;

    void Start()
    {
        _endGameButtons.OnClickAsObservable()
            .ThrottleFirst(TimeSpan.FromSeconds(1f))
            .TakeUntilDestroy(_endGameButtons)
            .Subscribe(_ => EndGame());
    }

    void EndGame()
    {
        EventData eventData = new EventData();
        eventData.Code = (byte)GameSate.Result;
        GameManager.Instance.OnEvent(eventData);
    }
}
