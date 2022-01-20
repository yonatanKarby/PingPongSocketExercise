using PingPong.Core;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
        private Person _person = new Person();
        public ClientTcp()
        {
            Console.WriteLine("What is the ip you want to connect to?");
            _ip = IPAddress.Parse(Console.ReadLine());
            Console.WriteLine("What is the port?");
            _port = int.Parse(Console.ReadLine());
            Console.WriteLine("Name");
            _person.name = Console.ReadLine();
            Console.WriteLine("Age:");
            _person.age = int.Parse(Console.ReadLine());
            _endpoint = new IPEndPoint(_ip, _port);
            _client = new TcpClient();
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
            _client.Connect(_endpoint);
            _stream = _client.GetStream();
            var personJson = JsonConvert.SerializeObject(_person);
            Send(Encoding.ASCII.GetBytes(personJson));
            Console.WriteLine("*** Connected ***");
        }
        private void ReceiveMessage()
        {
            var ReceiveBuffer = new byte[1024];
            _stream.Read(ReceiveBuffer);
            var message = Encoding.ASCII.GetString(ReceiveBuffer);
            Console.WriteLine(message);
        }
        private void SendMessage()
        {
            var buffer = GetIncodedMessege();
            Send(buffer);
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