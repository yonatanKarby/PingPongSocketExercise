using System;
using System.Threading.Tasks;

namespace Client
{
    public class ClientSocket : ITcpClient
    {
        public Task BeginWrite()
        {
            throw new NotImplementedException();
        }
    }
}
