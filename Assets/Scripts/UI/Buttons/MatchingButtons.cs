using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
// Photon 用の名前空間を参照する
using Photon.Pun;

/// <summary>
/// Title_MActhingButtonの制御
/// </summary>

public class MatchingButtons : MonoBehaviour
{
    [SerializeField] Button _cancelButton;

    const float WaitSeconds = 1f;

    void Start()
    {
        _cancelButton.OnClickAsObservable()
            .ThrottleFirst(TimeSpan.FromSeconds(WaitSeconds))
            .TakeUntilDestroy(_cancelButton)
            .Subscribe(_ => Cancel());
    }

    void Cancel()
    {
        BaseUI.Instance.ParentActive("Matching", false);
        PhotonNetwork.Disconnect();
    }
}
