using PingPong.Core;
using System.Net.Sockets;
using System.Text;

namespace PingPongExercise._tcp
{
    public class TcpServerConnection
    {
        private readonly TcpClient _client;
        private readonly IOutput<string> _output;
        private readonly string _name;
        private NetworkStream _stream;
        public TcpServerConnection(TcpClient client, string name)
        {
            _client = client;
            _name = name;
        }

        public void Start()
        {
            _stream = _client.GetStream();
            while(true)
            {
                try
                {
                    var buffer = Read();
                    write(buffer);
                }
                catch(System.IO.IOException ex)
                {
                    _output.Write($"Client {_name} disconnected");
                    return;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    _stream.Close();
                    _client.Close();
                }
            }
        }
        private byte[] Read()
        {
            byte[] buffer = new byte[1024];
            int length = _stream.Read(buffer, 0, buffer.Length);
            return buffer;
        }
        private void write(byte[] buffer)
        {
            var messege = Encoding.ASCII.GetString(buffer);
            _output.Write(messege);
        }
    }
}
