using Newtonsoft.Json;
using PingPong.Core;
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
        private Person _person;
        private bool _isRunning = true;

        public ClientSocket()
        {
            Console.WriteLine("What is the ip you want to connect to?");
            _ip = IPAddress.Parse(Console.ReadLine());
            Console.WriteLine("What is the port?");
            _port = int.Parse(Console.ReadLine());

            _endpoint = new IPEndPoint(_ip, _port);
            _socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
        }
        public async Task BeginWrite()
        {
            await Task.Run(() =>
            {
                Connect();
                while (_isRunning)
                {
                    SendMessage();
                    ReceiveMessage();                    
                }
            });
        }

        private void Connect()
        {
            Console.WriteLine("Connecting...");
            _socket.Connect(_endpoint);
            var personJson = JsonConvert.SerializeObject(_person);
            Send(Encoding.ASCII.GetBytes(personJson));
            Console.WriteLine("Connected");
        }

        private void SendMessage()
        {
            Console.WriteLine("Enter text:");
            var messege = Console.ReadLine();
            var buffer = Encoding.ASCII.GetBytes(messege);
            Send(buffer);
        }

        private void ReceiveMessage()
        {
            var receiveBuffer = Listen();
            var message = Encoding.ASCII.GetString(receiveBuffer);
            Console.WriteLine(message);
        }

        private byte[] Listen()
        {
            byte[] buffer = new byte[1024];
            _socket.Receive(buffer);
            return buffer;
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
