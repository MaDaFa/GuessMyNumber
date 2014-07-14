using Microsoft.AspNet.SignalR.Hubs;

namespace GuessMyNumber.Web
{
    public interface IGuessMyNumberManager
    {
        void Connect(string userName, string connectionId);

        void Reconnect(string userName, string connectionId);

        void SendMessage(string message, string connectionId);

        void Disconnect(string userName, string connectionId);
    }
}
