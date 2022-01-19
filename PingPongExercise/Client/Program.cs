namespace Client
{
    class Program
    {
        static async void Main(string[] args)
        {
            var port = 50000;
            var ip = new System.Net.IPAddress(new byte[] { 127,0,0,1 });
            ClientSocket client = new ClientSocket(ip, port);
            await client.BeginWrite();
        }
    }
}
