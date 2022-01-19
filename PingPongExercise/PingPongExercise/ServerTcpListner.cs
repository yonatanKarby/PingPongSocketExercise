using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PingPongExercise
{
    public class ServerTcpListner : ITcpServer
    {
        private readonly int _port;
        private readonly IPAddress _ip;
        private readonly TcpListener _listner;

        private bool IsRunning = true;
        
        public ServerTcpListner(IPAddress ip, int port)
        {
            _port = port;
            _ip = ip;
            _listner = new TcpListener(_ip, _port);
        }

        public async Task Listen()
        {
            await Task.Run(() =>
            {
                while (IsRunning)
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
