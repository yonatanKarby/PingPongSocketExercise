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
                Console.WriteLine("Connecting...");
                _client.Connect(_endpoint);
                Console.WriteLine("*** Connected ***");
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
            try
            {
                _stream.Write(buffer);
            }
            catch(System.IO.IOException ex)
            {
                Console.WriteLine($"Server not online {ex.Message}");
                _isRunning = false;
            }
            catch
            {
                throw;
            }
        }
        private byte[] GetIncodedMessege()
        {
            Console.Write("Enter text: ");
            var messege = Console.ReadLine();
            return Encoding.ASCII.GetBytes(messege);
        }
    }
}
