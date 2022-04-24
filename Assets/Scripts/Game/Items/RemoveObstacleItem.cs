using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

/// <summary>
/// ��莞��Obstacle�̐������~�߂�
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
