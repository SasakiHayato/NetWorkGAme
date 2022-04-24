using System.Collections.Generic;
using UnityEngine;
// Photon 用の名前空間を参照する
using Photon.Pun;

/// <summary>
/// Item全体の管理クラス
/// </summary>

public class ItemManager : MonoBehaviour, IManager, INetworkManager
{
    enum EffectTarget
    {
        My,
        Other,
    }

    [SerializeField] List<ItemData> _itemDatas;

    public PhotonView ManagerPhotonView { get ; set ; }

    [System.Serializable]
    class ItemData
    {
        public ItemBase ItemBase;
        public Sprite Sprite;
        public EffectTarget EffectTarget;
    }

    public void Request()
    {
        int random = Random.Range(0, _itemDatas.Count);

        switch (_itemDatas[random].EffectTarget)
        {
            case EffectTarget.My:
                CallBack(random);

                break;
            case EffectTarget.Other:
                ManagerPhotonView.RPC("CallBack", RpcTarget.Others, new object[] { random });

                break;
        }
    }

    [PunRPC]
    void CallBack(int itemID)
    {
        Debug.Log(itemID);

        _itemDatas[itemID].ItemBase.Use();
    }

    // IManager
    public GameObject ManagerObject() => gameObject;
    public string ManagerPath() => GetType().Name;
}
