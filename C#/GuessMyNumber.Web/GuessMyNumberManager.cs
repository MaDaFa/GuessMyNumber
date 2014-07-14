using Gamify.Sdk;
using Gamify.Sdk.Contracts.Notifications;
using Gamify.Sdk.Contracts.Requests;
using Gamify.Sdk.Services;
using Gamify.Sdk.Setup;
using GuessMyNumber.Core.Game.Setup;
using Microsoft.AspNet.SignalR;
using System.Linq;

namespace GuessMyNumber.Web
{
    public class GuessMyNumberManager : IGuessMyNumberManager
    {
        private readonly IGameInitializer gameInitializer;
        private readonly ISerializer serializer;
        private readonly IUserConnectionMapper userConnectionMapper;
        private readonly IHubContext<IGameHubClient> hubContext;

        private IGameService gameService;

        public GuessMyNumberManager(IGameInitializer gameInitializer, ISerializer serializer, IUserConnectionMapper userConnectionMapper)
        {
            this.gameInitializer = gameInitializer;
            this.serializer = serializer;
            this.userConnectionMapper = userConnectionMapper;
            this.hubContext = GlobalHost.ConnectionManager.GetHubContext<GuessMyNumberHub, IGameHubClient>();

            this.InitializeGameService();
        }

        public void Connect(string userName, string connectionId)
        {
            if (!this.userConnectionMapper.GetConnections(userName).Any())
            {
                this.gameService.Connect(userName);
            }

            this.userConnectionMapper.AddConnection(userName, connectionId);
        }

        public void Reconnect(string userName, string connectionId)
        {
            var connectionIds = this.userConnectionMapper.GetConnections(userName);

            if (!connectionIds.Contains(connectionId))
            {
                this.Connect(userName, connectionId);
            }
        }

        public void SendMessage(string message, string connectionId)
        {
            var gameRequest = this.serializer.Deserialize<GameRequest>(message);

            if (gameRequest.Type == (int)GameRequestType.PlayerConnect)
            {
                var errorMessage = "Player Connect message is not supported. Connection parameters must be set on initial SignalR hub connection";

                this.SendError(connectionId, errorMessage);
            }
            else
            {
                this.gameService.Send(message);
            }
        }

        public void Disconnect(string userName, string connectionId)
        {
            this.userConnectionMapper.RemoveConnection(userName, connectionId);

            if (!this.userConnectionMapper.GetConnections(userName).Any())
            {
                this.gameService.Disconnect(userName);
            }
        }

        private void InitializeGameService()
        {
            var gameDefinition = new GuessMyNumberDefinition();

            this.gameService = gameInitializer.Initialize(gameDefinition);

            this.gameService.Notification += (sender, args) =>
            {
                this.PushMessage(args.Receiver, args.Notification);
            };
        }

        private void PushMessage(string receiver, GameNotification notification)
        {
            var serializedNotification = this.serializer.Serialize(notification);
            var connectionIds = this.userConnectionMapper.GetConnections(receiver);

            foreach (var connectionId in connectionIds)
            {
                this.hubContext.Clients.Client(connectionId).PushMessage(serializedNotification);
            }
        }

        private void SendError(string connectionId, string errorMessage, params object[] parameters)
        {
            this.SendError(connectionId, 0, errorMessage, parameters);
        }

        private void SendError(string connectionId, int errorCode, string errorMessage, params object[] parameters)
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

            this.hubContext.Clients.Client(connectionId).PushMessage(serializedNotification);
        }
    }
}