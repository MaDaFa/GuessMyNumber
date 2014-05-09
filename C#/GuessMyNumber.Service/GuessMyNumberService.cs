﻿using Gamify.Sdk;
using Gamify.Service;
using Gamify.Service.Contracts.Notifications;
using Gamify.Service.Contracts.Requests;
using GuessMyNumber.Core;
using GuessMyNumber.Core.Game;
using GuessMyNumber.Core.Interfaces;
using GuessMyNumber.Server.Contracts;
using GuessMyNumber.Service.Contracts;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace GuessMyNumber.Server
{
    public class GuessMyNumberService : GamifyService
    {
        public GuessMyNumberService(IGameController controller)
            : base(controller)
        {
        }

        protected override IEnumerable<ISessionGamePlayerBase> GetSessionPlayers(CreateGameRequestObject createGameRequestObject)
        {
            var connectedPlayer1 = this.gameController.Players.First(p => p.UserName == createGameRequestObject.PlayerName);
            var sessionPlayer1 = new GuessMyNumberPlayer(connectedPlayer1);
            var connectedPlayer2 = this.gameController.Players.First(p => p.UserName == createGameRequestObject.InvitedPlayerName);
            var sessionPlayer2 = new GuessMyNumberPlayer(connectedPlayer2);

            var playerNumber = new Number(createGameRequestObject.AdditionalInformation);

            sessionPlayer1.AssignNumber(playerNumber);

            return new List<ISessionGamePlayerBase> { sessionPlayer1, sessionPlayer2 };
        }

        protected override void DecorateGameInvitation(GameInviteNotificationObject gameInviteNotificationObject)
        {
            var currentSession = this.gameController.Sessions.First(s => s.Id == gameInviteNotificationObject.SessionId);
            var sessionPlayer1 = currentSession.Player1 as GuessMyNumberPlayer;

            gameInviteNotificationObject.AdditionalInformation = sessionPlayer1.Number.ToString();
        }

        protected override void GetSessionPlayer2Ready(GameAcceptedRequestObject gameAcceptedRequestObject)
        {
            var currentSession = this.gameController.Sessions.First(s => s.Id == gameAcceptedRequestObject.SessionId);
            var sessionPlayer2 = currentSession.Player2 as GuessMyNumberPlayer;
            var player2Number = new Number(gameAcceptedRequestObject.AdditionalInformation);

            sessionPlayer2.AssignNumber(player2Number);
        }

        protected override void HandleGameMove(string serializedRequestObject)
        {
            var moveRequestObject = JsonConvert.DeserializeObject<GuessMyNumberMoveRequestObject>(serializedRequestObject);
            var number = new Number(moveRequestObject.Number);
            var move = new GuessMyNumberMove(number);
            var moveResponse = this.gameController.HandleMove<INumber, IAttemptResult>(moveRequestObject.PlayerName, moveRequestObject.SessionId, move);
            var currentSession = this.gameController.Sessions.First(s => s.Id == moveRequestObject.SessionId);

            if (moveResponse.MoveResponseObject.IsWinner())
            {
                var winnerPlayerName = moveRequestObject.PlayerName;
                var looserPlayerName = currentSession.Player1.Information.UserName == winnerPlayerName ?
                    currentSession.Player2.Information.UserName :
                    currentSession.Player1.Information.UserName;
                var gameFinishedNotificationObject = new GameFinishedNotificationObject
                {
                    SessionId = currentSession.Id,
                    WinnerPlayerName = winnerPlayerName,
                    LooserPlayerName = looserPlayerName
                };

                this.SendBroadcastNotification(GameNotificationType.GameFinished, gameFinishedNotificationObject, winnerPlayerName, looserPlayerName);

                return;
            }

            var originPlayer = currentSession.Player1.Information.UserName == moveRequestObject.PlayerName ?
                currentSession.Player1 :
                currentSession.Player2;
            var destinationPlayer = currentSession.Player1.Information.UserName == moveRequestObject.PlayerName ?
                currentSession.Player2 :
                currentSession.Player1;

            originPlayer.NeedsToMove = false;
            destinationPlayer.NeedsToMove = true;

            this.SendMoveNotification(moveRequestObject, destinationPlayer.Information.UserName, number);
            this.SendMoveResultNotification(moveRequestObject, moveResponse, destinationPlayer.Information.UserName, number);
        }

        protected override void SendGameInformation(string playerName, IGameSession gameSession)
        {
            var sessionPlayer1 = gameSession.Player1 as GuessMyNumberPlayer;
            var sessionPlayer2 = gameSession.Player2 as GuessMyNumberPlayer;
            var sessionPlayer1History = new PlayerHistoryObject(sessionPlayer1.Information.UserName);
            var sessionPlayer2History = new PlayerHistoryObject(sessionPlayer2.Information.UserName);

            foreach (var player2Move in sessionPlayer1.MovesHistory.Moves)
            {
                sessionPlayer2History.AddMove(new PlayerHistoryItemObject
                {
                    Number = player2Move.Response.Number.ToString(),
                    Goods = player2Move.Response.Goods,
                    Regulars = player2Move.Response.Regulars,
                    Bads = player2Move.Response.Bads
                });
            }

            foreach (var player1Move in sessionPlayer2.MovesHistory.Moves)
            {
                sessionPlayer1History.AddMove(new PlayerHistoryItemObject
                {
                    Number = player1Move.Response.Number.ToString(),
                    Goods = player1Move.Response.Goods,
                    Regulars = player1Move.Response.Regulars,
                    Bads = player1Move.Response.Bads
                });
            }

            var gameInformationNotificationObject = new GuessMyNumberGameInformationNotificationObject
            {
                SessionId = gameSession.Id,
                Player1History = sessionPlayer1History,
                Player2History = sessionPlayer2History
            };

            this.SendNotification(GameNotificationType.SendGameInformation, gameInformationNotificationObject, playerName);
        }

        private void SendMoveNotification(GuessMyNumberMoveRequestObject moveRequestObject, string destinationPlayerName, INumber number)
        {
            var gameMoveNotificationObject = new GuessMyNumberMoveNotificationObject
            {
                SessionId = moveRequestObject.SessionId,
                PlayerName = moveRequestObject.PlayerName,
                Number = number.ToString()
            };

            this.SendNotification(GameNotificationType.GameMove, gameMoveNotificationObject, destinationPlayerName);
        }

        private void SendMoveResultNotification(GuessMyNumberMoveRequestObject moveRequestObject, IGameMoveResponse<IAttemptResult> moveResponse, string destinationPlayerName, INumber number)
        {
            var gameMoveResultNotificationObject = new GuessMyNumberMoveResultNotificationObject
            {
                SessionId = moveRequestObject.SessionId,
                PlayerName = destinationPlayerName,
                Number = number.ToString(),
                Goods = moveResponse.MoveResponseObject.Goods,
                Regulars = moveResponse.MoveResponseObject.Regulars,
                Bads = moveResponse.MoveResponseObject.Bads
            };

            this.SendNotification(GameNotificationType.GameMoveResult, gameMoveResultNotificationObject, moveRequestObject.PlayerName);
        }
    }
}
