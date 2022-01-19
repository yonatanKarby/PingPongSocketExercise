using System.Threading.Tasks;

namespace Client
{
    public interface ITcpClient
    {
        Task BeginWrite();
    }
}
