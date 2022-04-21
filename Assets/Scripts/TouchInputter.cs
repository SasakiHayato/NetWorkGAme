using UnityEngine;
using System;

/// <summary>
/// 入力受付の管理クラス
/// </summary>

public class TouchInputter
{
    /// <summary>
    /// 入力データクラス
    /// </summary>
    public class InputData
    {
        /// <summary> マイフレームのスライド方向 </summary>
        public Vector2 UpdateNormalizeDir;

        /// <summary> 画面から離した際のスライド方向 </summary>
        public Vector2 ReleasedNormalizeDir;
    }

    Vector2 _pushPos = Vector2.zero;
    Vector2 _stayPos = Vector2.zero;

    Vector2 _saveStayPos = default;

    Action _pushEvents;
    Action _stayEvents;
    Action _releasedEvents;

    public InputData Data { get; private set; }

    const int ButtonID = 0;

    public void SetUp()
    {
        Data = new InputData();

        _pushEvents = null;
        _stayEvents = null;
        _releasedEvents = null;
    }

    /// <summary> 押した際のEventの登録 </summary>
    /// <param name="action">登録関数</param>
    public void AddPushEvent(Action action) => _pushEvents += action;

    /// <summary> 押している際のEventの登録 </summary>
    /// <param name="action">登録関数</param>
    public void AddStayEvent(Action action) => _stayEvents += action;

    /// <summary> 離した際のEventの登録 </summary>
    /// <param name="action">登録関数</param>
    public void AddReleasedEvent(Action action) => _releasedEvents += action;

    /// <summary>
    /// マイフレームの判定
    /// </summary>
    public void UpDate()
    {
        if (Input.GetMouseButtonDown(ButtonID)) Push();
        if (Input.GetMouseButton(ButtonID)) Stay();
        if (Input.GetMouseButtonUp(ButtonID)) Released();
    }

    void Push()
    {
        _pushEvents?.Invoke();

        _pushPos = Input.mousePosition;

        Data.ReleasedNormalizeDir = Vector2.zero;
    }

    void Stay()
    {
        _stayEvents?.Invoke();

        _stayPos = Input.mousePosition;

        if (_saveStayPos == default)
        {
            _saveStayPos = _stayPos;
        }
        else
        {
            Data.UpdateNormalizeDir = (_stayPos - _saveStayPos).normalized;
            _saveStayPos = _stayPos;
        }
    }

    void Released()
    {
        _releasedEvents?.Invoke();

        Data.ReleasedNormalizeDir = (_pushPos - _stayPos).normalized;

        Data.UpdateNormalizeDir = Vector2.zero;
        _saveStayPos = default;
    }
}
