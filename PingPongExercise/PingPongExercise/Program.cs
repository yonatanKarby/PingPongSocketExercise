using PingPong.Core;

namespace PingPongExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            var ip = new System.Net.IPAddress(new byte[] { 127, 0, 0, 1 });
            var port = 50000;
            var server = new ServerSocket(ip, port, new ConsoleOutput());
            server.ListenToNewUsers().GetAwaiter().GetResult();
        }
    }
}
