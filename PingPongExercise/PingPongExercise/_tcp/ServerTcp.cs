using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace PingPongExercise._tcp
{
    public class ServerTcp : ITcpServer
    {
        private readonly int _port;
        private readonly IPAddress _ip;
        private readonly TcpListener _listner;
        private readonly List<TcpServerConnection> _connections;
        public ServerTcp(IPAddress ip, int port)
        {
            _port = port;
            _ip = ip;
            _listner = new TcpListener(_ip, _port);
            _connections = new List<TcpServerConnection>();
        }
        public async Task ListenToNewUsers()
        {
            await Task.Run(() =>
            {
                _listner.Start();
                while (true)
                {
                    var client = _listner.AcceptTcpClient();
                    var connection = new TcpServerConnection(client, $"{_connections.Count}");
                    connection.Start();
                    _connections.Add(connection);
                }
            });
        }
    }
}
