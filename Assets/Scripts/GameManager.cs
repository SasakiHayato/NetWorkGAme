// Photon �p�̖��O��Ԃ��Q�Ƃ���
using ExitGames.Client.Photon;
using Photon.Realtime;

public enum GameSate : byte
{
    Start,
    End,
}

public class GameManager : IOnEventCallback
{
    public void OnEvent(EventData eventData)
    {

    }
}
