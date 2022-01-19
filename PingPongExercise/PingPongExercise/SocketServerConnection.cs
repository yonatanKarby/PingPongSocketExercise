using PingPong.Core;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace PingPongExercise
{
    public class SocketServerConnection
    {
        private readonly Socket _client;
        private readonly IOutput<string> _output;
        public SocketServerConnection(Socket client, IOutput<string> output)
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
                        ListedToClient();
                    }
                    catch(System.IO.IOException)
                    {
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
            _output.Write(messege);
        }
    }
}
