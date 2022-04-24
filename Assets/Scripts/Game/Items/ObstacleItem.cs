using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ‘ŠŽè‚ðŽ×–‚‚·‚éItem
/// </summary>

public class ObstacleItem : ItemBase
{
    [SerializeField] Sprite _obstacleSprite;
    [SerializeField] int _createCount;
    [SerializeField] ObstacleData _obstacleData;

    [System.Serializable]
    class ObstacleData
    {
        public float SetMinPositionX;
        public float SetMaxPositionX;
        public float SetPositionY;
        public float ObstacleAngle;
    }

    public override void Use()
    {
        for (int i = 0; i < _createCount; i++)
        {
            Execute(CraeteObstacle());
        }
    }

    GameObject CraeteObstacle()
    {
        GameObject obj = new GameObject(_obstacleSprite.name);
        
        SpriteRenderer spriteRenderer = obj.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = _obstacleSprite;
        spriteRenderer.sortingOrder = 999;

        return obj;
    }

    void Execute(GameObject obj)
    {
        
    }
}
