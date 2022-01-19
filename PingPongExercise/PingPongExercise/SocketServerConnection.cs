using PingPong.Core;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace PingPongExercise
{
    public class SocketServerConnection
    {
        private readonly Socket _client;
        private readonly string _name;
        private readonly IOutput<string> _output;
        public SocketServerConnection(Socket client, string name,IOutput<string> output)
        {
            _client = client;
            _output = output;
            _name = name;
        }

        public void Start()
        {
            new Thread(() =>
            {
                while(true)
                {
                    try
                    {
                        ListedToClient();
                    }
                    catch(SocketException ex)
                    {
                        _output.Write($"Client {_name} disconnected!");
                        return;
                    }
                    catch
                    {
                        throw;
                    }
                }
            }).Start();
        }

        private void ListedToClient()
        {
            var buffer = new byte[_client.ReceiveBufferSize];
            int length = _client.Receive(buffer);
            var messege = Encoding.ASCII.GetString(buffer);
            _output.Write($"{_name} -> {messege}");
        }
    }
}
