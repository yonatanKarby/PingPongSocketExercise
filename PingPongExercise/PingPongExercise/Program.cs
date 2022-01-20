using PingPong.Core;
using PingPongExercise._tcp;
using System;

namespace PingPongExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("No agrumnets passed should be \"Server.exe [port number] [ip number]\"");
            }
            else
            {
                int.TryParse(args[0], out int port);
                var ip = System.Net.IPAddress.Parse("127.0.0.1");
                var server = new ServerTcp(ip, port, new ConsoleOutput());
                server.ListenToNewUsers().GetAwaiter().GetResult();
            }
        }
    }
}
