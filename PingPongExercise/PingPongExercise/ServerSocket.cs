using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PingPongExercise
{
    public class ServerSocket
    {
        private readonly int _port;
        private readonly IPAddress _ip;
        private readonly Socket _socket;

        private bool _isRunning = true;

        public ServerSocket(IPAddress ip, int port)
        {
            _port = port;
            _ip = ip;
            var endpoint = new IPEndPoint(_ip, _port);
            _socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            _socket.Bind(endpoint);
        }

        public async Task Listen()
        {
            _socket.Listen(10);
            await Task.Run(() =>
            {
                Console.WriteLine("*** Waiting for connection... ***");
                var clientSocket = _socket.Accept();
                Console.WriteLine("*** Connected! ***");
                byte[] buffer = new byte[1024];

                while(_isRunning)
                {
                    int numofBytes = clientSocket.Receive(buffer);

                    var messege = Encoding.ASCII.GetString(buffer);
                    Console.WriteLine("*** New Message ***");
                    Console.WriteLine(messege);
                }
            });
        }
    }
}
