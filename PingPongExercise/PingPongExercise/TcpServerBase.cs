using PingPong.Core;
using System;
using System.Text;
using System.Threading.Tasks;

namespace PingPongExercise
{
    public abstract class TcpServerBase : ITcpServer
    {
        protected bool IsRunning = true;
        public readonly IOutput<string> _output;
        public abstract Task ListenToNewUsers();
        private string Parse(byte[] buffer)
        {
            return Encoding.ASCII.GetString(buffer);
        }
    }
}
