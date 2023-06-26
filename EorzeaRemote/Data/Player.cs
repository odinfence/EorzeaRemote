using Riptide;

namespace EorzeaRemote.Data
{
    internal class Player
    {
        public Connection Connection;
        public string Name;
        public bool IsAuthenticated;
        
        public ushort Id => Connection.Id;
        public bool IsConnecting => Connection.IsConnecting;

        public Player(Connection Connection)
        {
            this.Connection = Connection;
            this.Name = "Player Connecting..";
            this.IsAuthenticated = false;
        }
    }
}
