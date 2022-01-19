using System.Threading.Tasks;

namespace PingPongExercise
{
    public interface ITcpServer
    {
        Task Listen();
    }
}
