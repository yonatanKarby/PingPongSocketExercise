using PingPong.Core;
using PingPongExercise._tcp;
using System;

namespace PingPongExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("No agrumnets passed should be \"Server.exe [port number] [ip number]\"");
            }
            else
            {
                var port = int.Parse(args[0]);
                var ip = System.Net.IPAddress.Parse(args[1]);
                var server = new ServerTcp(ip, port, new ConsoleOutput());
                server.ListenToNewUsers().GetAwaiter().GetResult();
            }
        }
    }
}
