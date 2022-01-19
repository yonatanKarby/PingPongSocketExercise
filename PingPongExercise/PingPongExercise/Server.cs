using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PingPongExercise
{
    public class Server
    {
        private readonly int _port;
        private readonly IPAddress _ip;
        private readonly TcpListener _listner;

        private bool IsRunning = true;
        
        public Server(IPAddress ip, int port)
        {
            _port = port;
            _ip = ip;
            _listner = new TcpListener(_ip, _port);
        }

        public async void Listen()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    _listner.Start();
                    var client = _listner.AcceptTcpClient();

                    var stream = client.GetStream();
                    byte[] buffer = new byte[client.ReceiveBufferSize];

                    int length = stream.Read(buffer, 0, client.ReceiveBufferSize);
                    string messege = Encoding.ASCII.GetString(buffer, 0, length);

                    Console.WriteLine(messege);
                }
            });
        }
    }
}
