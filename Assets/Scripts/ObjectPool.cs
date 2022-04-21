using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ObjectPool�̊Ǘ��N���X
/// </summary>
/// <typeparam name="T"></typeparam>
/// 
public interface IPool
{
    bool IsUse { get; }
    void SetUp(Transform parent);
    void Delete();
}

public class ObjectPool<T> where T : Object, IPool
{
    Transform _parent;
    int _setCount;
    List<T> _pool = new List<T>();
    T _getT;

    /// <summary>
    /// Pool������Ώۂ̐ݒ�
    /// </summary>
    /// <param name="type">IPool</param>
    /// <param name="parent">Pool�̕ێ���</param>
    /// <param name="setCount">CreateCount</param>
    public void SetUp(T type, Transform parent = null, int setCount = 30)
    {
        _getT = type;
        _parent = parent;
        _setCount = setCount;

        Create();
    }

    void Create()
    {
        for (int i = 0; i < _setCount; i++)
        {
            T t = Object.Instantiate(_getT, _parent);
            t.SetUp(_parent);
            _pool.Add(t);
        }
    }

    /// <summary>
    /// �g���ĂȂ�Pool�̐\��
    /// </summary>
    /// <returns>�Ώ�Object</returns>
    public T Respons()
    {
        foreach (T t in _pool)
            if (!t.IsUse) return t;

        Create();
        return Respons();
    }
}
