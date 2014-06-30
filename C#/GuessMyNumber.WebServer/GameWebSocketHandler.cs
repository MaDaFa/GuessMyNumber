using Facebook;
using Gamify.Sdk;
using Gamify.Sdk.Contracts.Notifications;
using Gamify.Sdk.Contracts.Requests;
using Gamify.Sdk.Services;
using Gamify.Sdk.Setup;
using GuessMyNumber.Core.Game.Setup;
using Microsoft.Web.WebSockets;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace GuessMyNumber.WebServer
{
    public class GameWebSocketHandler : WebSocketHandler
    {
        private static ICollection<WebSocketHandler> connectedClients;

        private readonly IGameInitializer gameInitializer;
        private readonly ISerializer serializer;

        private IGameService gameService;

        public string UserName { get; private set; }

        static GameWebSocketHandler()
        {
            connectedClients = new WebSocketCollection();
        }

        public GameWebSocketHandler(IGameInitializer gameInitializer, ISerializer serializer)
        {
            this.gameInitializer = gameInitializer;
            this.serializer = serializer;
        }

        public override void OnOpen()
        {
            this.Initialize();

            connectedClients.Add(this);
        }

        public override void OnMessage(string message)
        {
            var gameRequest = this.serializer.Deserialize<GameRequest>(message);

            if (gameRequest.Type != (int)GameRequestType.PlayerConnect && string.IsNullOrEmpty(this.UserName))
            {
                this.SendError("A connection error occurred. The players must be connected in order to perform other type of requests");
            }

            if (gameRequest.Type == (int)GameRequestType.PlayerConnect && !string.IsNullOrEmpty(this.UserName))
            {
                this.SendError("A connection error occurred when trying to connect a player already connected");
            }

            if (gameRequest.Type == (int)GameRequestType.PlayerConnect)
            {
                var playerConnectRequest = this.serializer.Deserialize<PlayerConnectRequestObject>(gameRequest.SerializedRequestObject);

                if (this.AuthenticateUser(playerConnectRequest))
                {
                    this.UserName = playerConnectRequest.PlayerName;

                    this.gameService.Connect(playerConnectRequest.PlayerName);
                }
                else
                {
                    this.SendError("An error occured while trying to authenticate user {0}. Please check the format of the connect request", playerConnectRequest.PlayerName);
                }
            }
            else
            {
                this.gameService.Send(message);
            }
        }

        public override void OnClose()
        {
            base.OnClose();

            this.gameService.Disconnect(this.UserName);
            connectedClients.Remove(this);
        }

        private void Initialize()
        {
            var gameDefinition = new GuessMyNumberDefinition();

            this.gameService = gameInitializer.Initialize(gameDefinition);

            this.gameService.Notification += (sender, args) =>
            {
                this.PushMessage(args.Receiver, args.Notification);
            };
        }

        private bool AuthenticateUser(PlayerConnectRequestObject playerConnectRequest)
        {
            var isAuthenticated = false;
            var authenticationType = default(GameAuthenticationType);

            if (Enum.TryParse(playerConnectRequest.AuthenticationType.ToString(), out authenticationType))
            {
                switch (authenticationType)
                {
                    case GameAuthenticationType.Facebook:
                        var appId = ConfigurationManager.AppSettings["guessMyNumberAppId"];
                        var appSecret = ConfigurationManager.AppSettings["guessMyNumberAppSecret"];
                        var facebookClient = new FacebookClient
                        {
                            AppId = appId,
                            AppSecret = appSecret,
                            AccessToken = playerConnectRequest.AccessToken
                        };
                        var connectedUser = facebookClient.Get("me");

                        isAuthenticated = connectedUser != null;
                        break;
                    case GameAuthenticationType.None:
                        isAuthenticated = true;
                        break;
                }
            }

            return isAuthenticated;
        }

        private void PushMessage(string receiver, GameNotification notification)
        {
            var serializedNotification = this.serializer.Serialize(notification);
            var client = connectedClients
                .Cast<GameWebSocketHandler>()
                .FirstOrDefault(c => c.UserName == receiver);

            if (client != null)
            {
                client.Send(serializedNotification);
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
            var serializedErrorNotification = this.serializer.Serialize(errorNotification);

            this.Send(serializedErrorNotification);
        }
    }
}