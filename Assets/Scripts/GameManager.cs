// Photon 用の名前空間を参照する
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
