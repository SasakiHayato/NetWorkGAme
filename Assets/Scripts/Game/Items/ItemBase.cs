using UnityEngine;
// Photon �p�̖��O��Ԃ��Q�Ƃ���
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// Item�̊��N���X
/// </summary>

public abstract class ItemBase : MonoBehaviour
{
    public abstract void Use();
}
