namespace PingPong.Core
{
    public class Person
    {
        public string name { get; set; }
        public int age { get; set; }

        public override string ToString()
        {
            return $"age {age} name {name}";
        }
    }
}
