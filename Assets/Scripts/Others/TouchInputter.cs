using UnityEngine;
using System;

/// <summary>
/// ���͎�t�̊Ǘ��N���X
/// </summary>

public class TouchInputter
{
    /// <summary>
    /// ���̓f�[�^�N���X
    /// </summary>
    public class InputData
    {
        /// <summary> �}�C�t���[���̃X���C�h���� </summary>
        public Vector2 UpdateNormalizeDir;

        /// <summary> ��ʂ��痣�����ۂ̃X���C�h���� </summary>
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

    /// <summary> �������ۂ�Event�̓o�^ </summary>
    /// <param name="action">�o�^�֐�</param>
    public void AddPushEvent(Action action) => _pushEvents += action;

    /// <summary> �����Ă���ۂ�Event�̓o�^ </summary>
    /// <param name="action">�o�^�֐�</param>
    public void AddStayEvent(Action action) => _stayEvents += action;

    /// <summary> �������ۂ�Event�̓o�^ </summary>
    /// <param name="action">�o�^�֐�</param>
    public void AddReleasedEvent(Action action) => _releasedEvents += action;

    /// <summary>
    /// �}�C�t���[���̔���
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
