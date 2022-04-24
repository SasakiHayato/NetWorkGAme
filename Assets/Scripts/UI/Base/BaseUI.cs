using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

/// <summary>
/// UI管理のベースクラス
/// </summary>

public class BaseUI : SingletonAttribute<BaseUI>
{
    const string CanvasName = "BaseCanvas";
    List<ParentUI> _uiList;

    public override void SetUp()
    {
        base.SetUp();

        ParentUI[] parents = GameObject.Find(CanvasName).GetComponentsInChildren<ParentUI>();

        int id = 0;
        _uiList = new List<ParentUI>();

        foreach (ParentUI ui in parents)
        {
            ui.ID = id;
            ui.gameObject.GetComponent<Image>().raycastTarget = false;
            ui.CanvasGroup = ui.gameObject.AddComponent<CanvasGroup>();
            ui.SetUp();

            _uiList.Add(ui);

            id++;
        }
    }

    public void CallBackParent(int parentID, object[] data = null)
    {
        _uiList.First(u => u.ID == parentID).CallBack(data);
    }

    public void CallBackParent(string parentPath, object[] data = null)
    {
        _uiList.First(u => u.Path == parentPath).CallBack(data);
    }

    public void CallBack(int parentID, int childrenID, object[] data = null)
    {
        var parent = _uiList.First(u => u.ID == parentID);
        parent.UIList.First(u => u.ID == childrenID).CallBack(data);
    }

    public void CallBack(string parentPath, string childrenPath, object[] data = null)
    {
        var parent = _uiList.First(u => u.Path == parentPath);
        parent.UIList.First(u => u.Path == childrenPath).CallBack(data);
    }

    public void CallBack(int parentID, string childrenPath, object[] data = null)
    {
        var parent = _uiList.First(u => u.ID == parentID);
        parent.UIList.First(u => u.Path == childrenPath).CallBack(data);
    }

    public void CallBack(string parentPath, int childrenID, object[] data = null)
    {
        var parent = _uiList.First(u => u.Path == parentPath);
        parent.UIList.First(u => u.ID == childrenID).CallBack(data);
    }

    public void ParentActive(int id, bool active)
    {
        _uiList.First(u => u.ID == id).Active(active);
    }

    public void ParentActive(string path, bool active)
    {
        _uiList.First(u => u.Path == path).Active(active);
    }

    public void AtParentActive(int id)
    {
        foreach (ParentUI ui in _uiList)
        {
            if (ui.ID == id) ui.Active(true);
            else ui.Active(false);
        }
    }

    public void AtParantActive(string path)
    {
        foreach (ParentUI ui in _uiList)
        {
            if (ui.Path == path) ui.Active(true);
            else ui.Active(false);
        }
    }

    public GameObject GetParent(int id)
    {
        return _uiList.First(u => u.ID == id).Parent;
    }

    public GameObject GetParent(string path)
    {
        return _uiList.First(u => u.Path == path).Parent;
    }
}
