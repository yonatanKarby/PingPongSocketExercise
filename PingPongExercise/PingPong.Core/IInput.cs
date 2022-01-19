namespace PingPong.Core
{
    public interface IInput<T>
    {
        T Read();
    }
}
