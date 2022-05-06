using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// RankIn���̖��O�̐ݒ�
/// </summary>

public class NameInputter : MonoBehaviour
{
    [SerializeField] int _nameCapcity;
    [SerializeField] InputField _inputField;

    public string Name { get; private set; }

    public bool SetName()
    {
        string name = _inputField.text;

        if (!NameCheck(name))
        {
            BaseUI.Instance.CallBack("Entry", "Warning");
            return false;
        }
       
        Name = name;

        return true;
    }

    bool NameCheck(string name)
    {
        if (name.Length > _nameCapcity) return false;
        if (name.Length == 0) return false;

        return true;
    }
}
