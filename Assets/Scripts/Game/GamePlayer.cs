using UnityEngine;

/// <summary>
/// Player�𐧌䂷��N���X
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
