using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Item全体の管理クラス
/// </summary>

public class ItemManager : MonoBehaviour, IManager
{
    [SerializeField] List<ItemData> _itemDatas;

    [System.Serializable]
    class ItemData
    {
        public ItemBase ItemBase;
        public Sprite Sprite;
    }

    public void Request()
    {
        int random = Random.Range(0, _itemDatas.Count);
        _itemDatas[random].ItemBase.Use();
    }

    // IManager
    public Object ManagerObject() => gameObject;
    public string ManagerPath() => GetType().Name;
}
