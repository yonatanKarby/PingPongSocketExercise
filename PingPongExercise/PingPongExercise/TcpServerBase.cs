using System.Threading.Tasks;

namespace PingPongExercise
{
    public abstract class TcpServerBase : ITcpServer
    {
        public abstract Task ListenToNewUsers();
    }
}
