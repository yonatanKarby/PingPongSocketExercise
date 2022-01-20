using Newtonsoft.Json;
using PingPong.Core;
using System;
using System.Net.Sockets;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;

namespace PingPongExercise._tcp
{
    public class TcpServerConnection
    {
        private readonly TcpClient _client;
        private readonly IOutput<string> _output;
        private Person _person;
        private NetworkStream _stream;
        public TcpServerConnection(TcpClient client, IOutput<string> output)
        {
            _client = client;
            _output = output;
        }

        public void Start()
        {
            new Thread(() =>
            {
                _stream = _client.GetStream();
                GetPerson();
                while (true)
                {
                    try
                    {
                        var buffer = ListenToClient();
                        SendClient(buffer);
                    }
                    catch (System.IO.IOException ex)
                    {
                        _output.Write($"Client {_person.ToString()} disconnected + {ex.Message}");
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
            _stream.Read(buffer);
            var json = Encoding.ASCII.GetString(buffer);
            _person = JsonConvert.DeserializeObject<Person>(json);
        }
        private void SendClient(byte[] buffer)
        {
            Console.WriteLine("*** Sending back ***");
            _stream.Write(buffer);
        }
        private byte[] ListenToClient()
        {
            var buffer = Read();
            write(buffer);
            return buffer;
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
            _output.Write($"{_person.ToString()} -> {messege}");
        }
    }
}