using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class ClientSocket : ITcpClient
    {
        private readonly IPAddress _ip;
        private readonly int _port;
        private readonly IPEndPoint _endpoint;
        private readonly Socket _socket;

        private bool _isRunning = true;

        public ClientSocket()
        {
            Console.WriteLine("What is the ip you want to connect to?");
            _ip = System.Net.IPAddress.Parse(Console.ReadLine());
            Console.WriteLine("What is the port?");
            _port = int.Parse(Console.ReadLine());

            _endpoint = new IPEndPoint(_ip, _port);
            _socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
        }
        public async Task BeginWrite()
        {
            await Task.Run(() =>
            {
                Console.WriteLine("Connecting...");
                _socket.Connect(_endpoint);
                Console.WriteLine("Connected");

                while (_isRunning)
                {
                    Console.WriteLine("Enter text:");
                    var messege = Console.ReadLine();
                    var buffer = Encoding.ASCII.GetBytes(messege);
                    Send(buffer);
                }
            });
        }
        private void Send(byte[] buffer)
        {
            try
            {
                _socket.Send(buffer);
            }
            catch(SocketException ex)
            {
                Console.WriteLine($"Server not online {ex.Message}");
                _isRunning = false;
            }
            catch
            {
                throw;
            }
        }
    }
}
