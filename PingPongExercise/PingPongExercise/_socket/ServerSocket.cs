﻿using PingPong.Core;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace PingPongExercise._socket
{
    public class ServerSocket : TcpServerBase
    {
        private readonly int _port;
        private readonly IPAddress _ip;
        private readonly Socket _socket;
        private readonly IOutput<string> _output;
        private List<SocketServerConnection> _connections;

        public ServerSocket(IPAddress ip, int port, IOutput<string> output)
        {
            _port = port;
            _ip = ip;
            _output = output;
            _connections = new List<SocketServerConnection>();
            var endpoint = new IPEndPoint(_ip, _port);
            _socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            _socket.Bind(endpoint);
        }

        public override async Task ListenToNewUsers()
        {
            _output.Write("server online");
            await Task.Run(() =>
            {
                _socket.Listen(10);
                while (true)
                {
                    var clientSocket = _socket.Accept();
                    _output.Write("New Connection!");
                    var connection = new SocketServerConnection(clientSocket,_output);
                    connection.Start();
                    _connections.Add(connection);
                }
            });
        }
    }
}
