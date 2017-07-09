namespace Loom
{
    public static class Singleton<T> where T : new()
    {
        public static readonly T Get;

        static Singleton()
        {
            Get = new T();
        }
    }
}