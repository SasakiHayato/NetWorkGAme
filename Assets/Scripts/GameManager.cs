// Photon —p‚Ì–¼‘O‹óŠÔ‚ğQÆ‚·‚é
using ExitGames.Client.Photon;
using Photon.Realtime;

public enum GameSate : byte
{
    Start,
    End,
}

public class GameManager : SingletonAttribute<GameManager>, IOnEventCallback
{
    public override void SetUp()
    {
        base.SetUp();

    }

    public void OnEvent(EventData eventData)
    {

    }
}
