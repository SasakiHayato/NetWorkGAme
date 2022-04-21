using UnityEngine;

/// <summary>
/// Player‚ğ§Œä‚·‚éƒNƒ‰ƒX
/// </summary>

public class GamePlayer : MonoBehaviour
{
    TouchInputter _inputter = new TouchInputter();

    void Start()
    {
        _inputter.SetUp();
    }

    void Update()
    {
        _inputter.UpDate();
    }
}
