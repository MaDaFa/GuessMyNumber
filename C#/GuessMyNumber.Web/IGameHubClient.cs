namespace GuessMyNumber.Web
{
    public interface IGameHubClient
    {
        void PushMessage(string serializedNotification);
    }
}
