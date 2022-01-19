using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PingPongExercise._tcp
{
    public class ServerTcp : ITcpServer
    {
        private readonly int _port;
        private readonly IPAddress _ip;
        private readonly TcpListener _listner;
        public ServerTcp(IPAddress ip, int port)
        {
            _port = port;
            _ip = ip;
            _listner = new TcpListener(_ip, _port);
        }
        public async Task ListenToNewUsers()
        {
            await Task.Run(() =>
            {
                _listner.Start();
                while (true)
                {
                    var client = _listner.AcceptTcpClient();
                }
            });
        }
    }
}
