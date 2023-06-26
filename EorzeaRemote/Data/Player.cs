using Riptide;

namespace EorzeaRemote.Data
{
    internal class Player
    {
        public Connection Connection;
        public string Name;
        private bool Authenticated;
        
        public ushort Id => Connection.Id;
        public bool IsConnected => Connection.IsConnected;
        public bool IsAuthenticated
        {
            get { return Authenticated && IsConnected; }
            set { Authenticated = value; }
        }

        public Player(Connection Connection)
        {
            this.Connection = Connection;
            this.Name = "Player Connecting..";
            this.Authenticated = false;
        }
    }
}
