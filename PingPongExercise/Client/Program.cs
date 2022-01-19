using System;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new ClientTcp();
            client.BeginWrite().GetAwaiter().GetResult();
        }
    }
}
