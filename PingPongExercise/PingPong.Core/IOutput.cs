namespace PingPong.Core
{
    public interface IOutput<T>
    {
        void Write(T obj);
    }
}
