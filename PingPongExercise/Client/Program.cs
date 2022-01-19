using System;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length < 2)
            {
                Console.WriteLine("No agrumnets passed should be \"Client.exe [port number] [ip number]\"");
            }
            else
            {
                var port = int.Parse(args[0]);
                var ip = System.Net.IPAddress.Parse(args[1]);
                var client = new ClientTcp(ip, port);
                client.BeginWrite().GetAwaiter().GetResult();
            }
        }
    }
}
