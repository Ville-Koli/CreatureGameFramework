namespace Framework.Game
{
    public class Source<T>
    {
        private T source;
        public Source(T source)
        {
            this.source = source;
        }
        public T GetSource() => source;
    }
}