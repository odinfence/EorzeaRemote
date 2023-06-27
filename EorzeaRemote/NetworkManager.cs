using EorzeaRemote.Data;
using Riptide;
using Riptide.Utils;
using System.Timers;
using EorzeaRemote.Handlers;

namespace EorzeaRemote
{
    internal class NetworkManager
    {
        private readonly Server Server;
        private readonly Dictionary<ushort, Player> Players;

        private readonly MessageHandler MessageHandler;
        
        public NetworkManager(ushort port = 7777, ushort maxClients = 255)
        {
            RiptideLogger.Initialize(Console.WriteLine, true);

            Players = new();
            Server = new();
            Server.ClientConnected += ClientConnected;
            Server.ClientDisconnected += ClientDisconnected;
            Server.MessageReceived += MessageReceived;

            MessageHandler = new(Server, Players);

            var updateLoop = new System.Timers.Timer
            {
                Interval = 60,
                AutoReset = true
            };
            updateLoop.Elapsed += Update;
            updateLoop.Enabled = true;

            Server.Start(port, maxClients);
        }

        private void ClientConnected(object? sender, ServerConnectedEventArgs e)
        {
            var Player = new Player(e.Client);
            Players.Add(e.Client.Id, Player);
        }

        private void ClientDisconnected(object? sender, ServerDisconnectedEventArgs e)
        {
            Players.Remove(e.Client.Id);

            var Response = Message.Create(MessageSendMode.Reliable, MessageSendType.PlayerDisconnected);
            Response.AddUShort(e.Client.Id);

            Server.SendToAll(Response);
        }

        private void MessageReceived(object? sender, MessageReceivedEventArgs e)
        {
            MessageHandler.Handle(e);
        }

        private void Update(object? sender, ElapsedEventArgs e)
        {
            Server.Update();
        }
    }
}
