using UnityEngine;

/// <summary>
/// SoroÉQÅ[ÉÄÇÃç€ÇÃBotêßå‰ÉNÉâÉX
/// </summary>

public class BotPlayer : MonoBehaviour
{
    [SerializeField] Transform _judgePrefab;

    bool _isSetUp = false;

    GameObject _judgePrefabClone;

    void Update()
    {
        if (!_isSetUp) return;
    }

    public void SetUp()
    {
        GameObject obj = new GameObject(_judgePrefab.name + "ToBot");
        obj.transform.position = _judgePrefab.position;
        _judgePrefabClone = obj;

        _isSetUp = true;
    }
}
