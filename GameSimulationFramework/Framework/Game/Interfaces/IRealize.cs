namespace Framework.Game.Interfaces
{
    public interface IRealize
    {
        public object?[]? RealizeValue(int amount);
    }
    public interface IRealize<T> : IRealize
    {
        public new T?[]? RealizeValue(int amount);
    }
}