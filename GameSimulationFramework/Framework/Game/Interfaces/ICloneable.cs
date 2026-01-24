namespace Framework.Game.Interfaces
{
    public interface IFrameworkCloneable
    {
        public object? ObjectClone();
    }
    public interface IFrameworkCloneable<T> : IFrameworkCloneable
    {
        public T Clone();
    }
}