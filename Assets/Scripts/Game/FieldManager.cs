using UnityEngine;

/// <summary>
/// ゲームフィールド内の全体管理クラス
/// </summary>

public class FieldManager : MonoBehaviour
{
    [SerializeField] ObjectDataBase _objectData;
    [SerializeField] string _isDebugObjectDataPath;
    [SerializeField] float _playSecondTime;

    void Start()
    {
        SetObject();
    }

    void SetObject()
    {
        ObjectData objectData = null;

        if (_isDebugObjectDataPath != "")
        {
            objectData = _objectData.GetData(_isDebugObjectDataPath);
        }

        GameObject obj = new GameObject($"{objectData.Path}");
        SpriteRenderer spriteRenderer = obj.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = objectData.Sprite;

        obj.transform.position = Vector2.zero;
    }
}
