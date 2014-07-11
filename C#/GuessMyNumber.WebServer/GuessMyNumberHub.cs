using Gamify.Sdk;
using Gamify.Sdk.Contracts.Notifications;
using Gamify.Sdk.Contracts.Requests;
using Gamify.Sdk.Services;
using Gamify.Sdk.Setup;
using GuessMyNumber.Core.Game.Setup;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Linq;
using System.Threading.Tasks;

namespace GuessMyNumber.WebServer
{
    [HubName("GuessMyNumberHub")]
    [GameAuthorize]
    public class GuessMyNumberHub : Hub
    {
        private readonly IGameInitializer gameInitializer;
        private readonly ISerializer serializer;
        private readonly IUserConnectionMapper userConnectionMapper;

        private IGameService gameService;

        public GuessMyNumberHub(IGameInitializer gameInitializer, ISerializer serializer, IUserConnectionMapper userConnectionMapper)
        {
            this.gameInitializer = gameInitializer;
            this.serializer = serializer;
            this.userConnectionMapper = userConnectionMapper;
        }

        public override Task OnConnected()
        {
            var userName = Context.User.Identity.Name;

            this.userConnectionMapper.AddConnection(userName, Context.ConnectionId);

            var gameDefinition = new GuessMyNumberDefinition();

            this.gameService = gameInitializer.Initialize(gameDefinition);

            this.gameService.Notification += (sender, args) =>
            {
                this.PushMessage(args.Receiver, args.Notification);
            };

            this.gameService.Connect(userName);

            return base.OnConnected();
        }

        public void SendMessage(string message)
        {
            var gameRequest = this.serializer.Deserialize<GameRequest>(message);

            if (gameRequest.Type == (int)GameRequestType.PlayerConnect)
            {
                this.SendError("Player Connect message is not supported. Connection parameters must be set on initial SignalR hub connection");
            }
            else
            {
                this.gameService.Send(message);
            }
        }

        public override Task OnReconnected()
        {
            var userName = Context.User.Identity.Name;
            var connectionIds = this.userConnectionMapper.GetConnections(userName);

            if (!connectionIds.Contains(Context.ConnectionId))
            {
                this.userConnectionMapper.AddConnection(userName, Context.ConnectionId);
            }

            return base.OnReconnected();
        }

        public override Task OnDisconnected()
        {
            var userName = Context.User.Identity.Name;

            this.userConnectionMapper.RemoveConnection(userName, Context.ConnectionId);

            return base.OnDisconnected();
        }

        private void PushMessage(string receiver, GameNotification notification)
        {
            var serializedNotification = this.serializer.Serialize(notification);
            var userName = Context.User.Identity.Name;

            if (userName == receiver)
            {
                var connectionIds = this.userConnectionMapper.GetConnections(userName);

                foreach (var connectionId in connectionIds)
                {
                    Clients.Client(connectionId).PushMessage(serializedNotification);
                }
            }
        }

        private void SendError(string errorMessage, params object[] parameters)
        {
            this.SendError(0, errorMessage, parameters);
        }

        private void SendError(int errorCode, string errorMessage, params object[] parameters)
        {
            var errorNotificationObject = new ErrorNotificationObject
            {
                ErrorCode = errorCode,
                Message = string.Format(errorMessage, parameters)
            };
            var errorNotification = new GameNotification
            {
                Type = (int)GameNotificationType.Error,
                SerializedNotificationObject = this.serializer.Serialize(errorNotificationObject)

            };
            var serializedNotification = this.serializer.Serialize(errorNotification);

            Clients.Caller.PushMessage(serializedNotification);
        }
    }
}