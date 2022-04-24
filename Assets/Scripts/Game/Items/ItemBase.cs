using UnityEngine;
// Photon 用の名前空間を参照する
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// Itemの基底クラス
/// </summary>

public abstract class ItemBase : MonoBehaviour
{
    public abstract void Use();
}
