using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
// Photon —p‚Ì–¼‘O‹óŠÔ‚ğQÆ‚·‚é
using Photon.Pun;

/// <summary>
/// Title_MActhingButton‚Ì§Œä
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
