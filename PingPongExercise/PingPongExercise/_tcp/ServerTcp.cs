using PingPong.Core;
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
        private readonly IOutput<string> _output;
        public ServerTcp(IPAddress ip, int port, IOutput<string> output)
        {
            _port = port;
            _ip = ip;
            _output = output;
            _listner = new TcpListener(_ip, _port);
            _connections = new List<TcpServerConnection>();
        }
        public async Task ListenToNewUsers()
        {
            await Task.Run(() =>
            {
                _output.Write("server online...");
                _listner.Start();
                while (true)
                {
                    var client = _listner.AcceptTcpClient();
                    _output.Write("New Connection!");
                    var connection = new TcpServerConnection(client, $"{_connections.Count}", _output);
                    connection.Start();
                    _connections.Add(connection);
                }
            });
        }
    }
}
