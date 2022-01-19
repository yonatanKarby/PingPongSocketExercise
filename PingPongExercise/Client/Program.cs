namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var port = 50000;
            var ip = new System.Net.IPAddress(new byte[] { 127,0,0,1 });
            var client = new ClientTcp(ip, port);
            client.BeginWrite().GetAwaiter().GetResult();
        }
    }
}
