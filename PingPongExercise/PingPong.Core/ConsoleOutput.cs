using System;

namespace PingPong.Core
{
    public class ConsoleOutput : IOutput<string>
    {
        public void Write(string obj)
        {
            Console.WriteLine(obj);
        }
    }
}
