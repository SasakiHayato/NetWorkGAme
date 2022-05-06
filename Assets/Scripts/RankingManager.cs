using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using NCMB;

/// <summary>
/// Rankingの管理クラス
/// </summary>

public class RankingManager : MonoBehaviour, IManager
{
    bool _isEntry = false;

    NCMBSettings _ncmbSettings = null;
    NameInputter _nameInputter;

    const string ApplicationKey = "6714acef7bd42fcae8865824a5fc9a0ab96cac013309682955832c5679982bdb";
    const string ClientKey = "b9682e97c53f136cc2c6f2882df32682dfa0fb45ac69236c99d50bcaae74f783";

    const string DataStore = "Ranking";
    const string Score = "Score";
    const string User = "Name";
    const int TopRanking = 10;

    void Start()
    {
        _nameInputter = FindObjectOfType<NameInputter>();
    }

    public void SetUp()
    {
        _isEntry = false;

        if (_ncmbSettings == null)
        {
            CreateNCMBSetting();
        }
    }

    void CreateNCMBSetting()
    {
        GameObject obj = new GameObject("NCMBSetting");

        _ncmbSettings = obj.AddComponent<NCMBSettings>();

        _ncmbSettings.applicationKey = ApplicationKey;
        _ncmbSettings.clientKey = ClientKey;
    }

    /// <summary>
    /// ランキングの清算
    /// </summary>
    public void CallBackRankingData()
    {
        int score = GameManager.Instance.ScoreManager.CurrentScore;

        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(DataStore);
        query.OrderByDescending(Score);
        query.Limit = TopRanking;

        query.FindAsync((ncmbList, e) => 
        {
            if (e != null) Debug.Log(e.ErrorMessage);
            else RankInCheck(ncmbList, score);
        });
    }

    void RankInCheck(List<NCMBObject> ncmbList, int score)
    {
        if (score > 0 && ncmbList.Count < TopRanking 
            || score > int.Parse(ncmbList[ncmbList.Count - 1][Score].ToString()))
        {
            // RankIn.
            WaitSetData(score).Forget();
        }
        else
        {
            // not
        }
    }

    // Buttonからの呼び出し
    public void Entry()
    {
        _isEntry = _nameInputter.SetName();
        BaseUI.Instance.CallBack("Entry", "EntryPanelAnim", new object[] { false });
    }

    async UniTask WaitSetData(int score)
    {
        // 名前入力
        BaseUI.Instance.ParentActive("Entry", true);
        BaseUI.Instance.CallBack("Entry", "EntryPanelAnim", new object[] { true });

        await UniTask.WaitUntil(() => _isEntry);
        Save(score, _nameInputter.Name);
    }

    public void Load()
    {

    }

    void Save(int score, string name)
    {
        NCMBObject ncmbObject = new NCMBObject(DataStore);
        ncmbObject[User] = name;
        ncmbObject[Score] = score;

        ncmbObject.SaveAsync(e =>
        {
            if (e != null) Debug.Log(e.ErrorMessage);
            else
            {
                NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(DataStore);
                query.FindAsync((objList, e) =>
                {
                    if (e != null) Debug.Log($"Can't Find \n {e.ErrorMessage}");
                    else
                    {

                    }
                });
            }
        });
    }

    // IManager
    public GameObject ManagerObject() => gameObject;
    public string ManagerPath() => GetType().Name;
}
