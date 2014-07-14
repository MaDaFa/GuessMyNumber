using Microsoft.AspNet.SignalR;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GuessMyNumber.Web
{
    [GameAuthorize]
    public class GuessMyNumberHub : Hub<IGameHubClient>
    {
        private readonly IGuessMyNumberManager manager;

        public GuessMyNumberHub(IGuessMyNumberManager manager)
        {
            this.manager = manager;
        }

        public override Task OnConnected()
        {
            var userName = this.GetUserName();

            this.manager.Connect(userName, Context.ConnectionId);

            return base.OnConnected();
        }

        public void SendMessage(string message)
        {
            this.manager.SendMessage(message, Context.ConnectionId);
        }

        public override Task OnReconnected()
        {
            var userName = this.GetUserName();

            this.manager.Reconnect(userName, Context.ConnectionId);

            return base.OnReconnected();
        }

        public override Task OnDisconnected()
        {
            var userName = this.GetUserName();

            this.manager.Disconnect(userName, Context.ConnectionId);

            return base.OnDisconnected();
        }

        private string GetUserName()
        {
            var userName = Context.User.Identity.Name;

            if (string.IsNullOrEmpty(userName) && Context.Request.Environment.ContainsKey("authorizedUser"))
            {
                var principal = Context.Request.Environment["authorizedUser"] as ClaimsPrincipal;

                userName = principal.Identity.Name;
            }

            return userName;
        }
    }
}