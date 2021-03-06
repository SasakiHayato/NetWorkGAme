using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

/// <summary>
/// 一定時間Obstacleの生成を止める
/// </summary>

public class RemoveObstacleItem : ItemBase
{
    [SerializeField] float _effectTime;

    public override void Use()
    {
        GameManager.Instance.FieldManager.IsRemoveObstacle = true;
        EndEffect().Forget();
    }

    async UniTask EndEffect()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_effectTime));
        GameManager.Instance.FieldManager.IsRemoveObstacle = false;
    }
}
