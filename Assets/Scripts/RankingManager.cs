using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;

public class RankingManager : MonoBehaviour, IManager
{
    const string ApplicationKey = "6714acef7bd42fcae8865824a5fc9a0ab96cac013309682955832c5679982bdb";
    const string ClientKey = "b9682e97c53f136cc2c6f2882df32682dfa0fb45ac69236c99d50bcaae74f783";

    const string DataStore = "Ranking";
    const string Score = "Score";
    const string User = "Name";

    public void SetUp()
    {
        Create();
    }

    void Create()
    {
        GameObject obj = new GameObject("NCMBSetting");

        NCMBSettings ncmb = obj.AddComponent<NCMBSettings>();

        ncmb.applicationKey = ApplicationKey;
        ncmb.clientKey = ClientKey;
    }

    public void Load()
    {

    }

    public GameObject ManagerObject() => gameObject;
    public string ManagerPath() => GetType().Name;
}
