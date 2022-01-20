using System;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new ClientSocket();
            client.BeginWrite().GetAwaiter().GetResult();
        }
    }
}
