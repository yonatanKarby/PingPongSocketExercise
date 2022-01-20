using Newtonsoft.Json;
using PingPong.Core;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace PingPongExercise._socket
{
    public class SocketServerConnection
    {
        private readonly Socket _client;
        private readonly string _name;
        private readonly IOutput<string> _output;
        private Person _person;
        public SocketServerConnection(Socket client,IOutput<string> output)
        {
            _client = client;
            _output = output;
        }

        public void Start()
        {
            new Thread(() =>
            {
                while(true)
                {
                    try
                    {
                        var buffer = ListedToClient();
                        SendClient(buffer);
                    }
                    catch(SocketException ex)
                    {
                        _output.Write($"Client {_person.ToString()} disconnected! + {ex.Message}");
                        return;
                    }
                    catch
                    {
                        throw;
                    }
                }
            }).Start();
        }

        private void GetPerson()
        {
            var buffer = new byte[1024];
            _client.Receive(buffer);
            var json = Encoding.ASCII.GetString(buffer);
            _person = JsonConvert.DeserializeObject<Person>(json);
        }

        private byte[] ListedToClient()
        {
            var buffer = new byte[_client.ReceiveBufferSize];
            int length = _client.Receive(buffer);
            var messege = Encoding.ASCII.GetString(buffer);
            _output.Write($"{_person.ToString()} -> {messege}");
            return buffer;
        }
        private void SendClient(byte[] buffer)
        {
            _client.Send(buffer);
        }
    }
}
