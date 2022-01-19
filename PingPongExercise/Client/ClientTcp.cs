using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class ClientTcp : ITcpClient
    {
        private readonly IPAddress _ip;
        private readonly int _port;
        private readonly TcpClient _client;
        private readonly IPEndPoint _endpoint;
        private NetworkStream _stream;
        private bool _isRunning = true;
        public ClientTcp(IPAddress ip, int port)
        {
            _port = port;
            _ip = ip;
            _endpoint = new IPEndPoint(_ip, _port);
            _client = new TcpClient();
        }
        public async Task BeginWrite()
        {
            await Task.Run(() =>
            {
                _client.Connect(_endpoint);
                _stream = _client.GetStream();
                while (_isRunning)
                {
                    var buffer = GetIncodedMessege();
                    Send(buffer);
                }
            });
        }
        private void Send(byte[] buffer)
        {
            _stream.Write(buffer);
        }
        private byte[] GetIncodedMessege()
        {
            var messege = Console.ReadLine();
            return Encoding.ASCII.GetBytes(messege);
        }
    }
}
