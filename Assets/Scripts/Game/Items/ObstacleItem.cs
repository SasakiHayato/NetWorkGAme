using UnityEngine;
using DG.Tweening;

/// <summary>
/// ‘ŠŽè‚ðŽ×–‚‚·‚éItem
/// </summary>

public class ObstacleItem : ItemBase
{
    [SerializeField] Sprite _obstacleSprite;
    [SerializeField] int _createCount;
    [SerializeField] float _moveDuration;
    [SerializeField] ObstacleData _obstacleData;

    [System.Serializable]
    class ObstacleData
    {
        public float SetMinPositionX;
        public float SetMaxPositionX;
        public float SetPositionY;
        public float ObstacleAngle;
        public float MoveDalayMinTime;
        public float MoveDalayMaxTime;
        public float MoveDistance;
    }

    public override void Use()
    {
        for (int i = 0; i < _createCount; i++)
        {
            Execute(CraeteObstacle().transform);
        }
    }

    GameObject CraeteObstacle()
    {
        GameObject obj = new GameObject(_obstacleSprite.name);

        float posX = Random.Range(_obstacleData.SetMinPositionX, _obstacleData.SetMaxPositionX);
        obj.transform.position = new Vector2(posX, _obstacleData.SetPositionY);
        obj.transform.rotation = Quaternion.Euler(0, 0, _obstacleData.ObstacleAngle);
        
        SpriteRenderer spriteRenderer = obj.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = _obstacleSprite;
        spriteRenderer.sortingOrder = 999;

        return obj;
    }

    void Execute(Transform t)
    {
        float delay = Random.Range(_obstacleData.MoveDalayMinTime, _obstacleData.MoveDalayMaxTime);
        Vector2 endPos = (t.right * -1) * _obstacleData.MoveDistance + t.position;

        t.DOMove(endPos, _moveDuration)
            .SetEase(Ease.Linear)
            .SetDelay(delay)
            .OnComplete(() => Destroy(t.gameObject));
    }
}
