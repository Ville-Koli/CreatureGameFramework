using System.Collections.Immutable;
using Framework.Game.GameEventArgs;

namespace Framework.Game.Teams.Creatures.Components
{
    public class ComponentRange<T>
    {
        private IEnumerable<T> _range;
        public ComponentRange(IEnumerable<T> rangeOfElements)
        {
            _range = rangeOfElements;
        }
        public T? GetMin() => _range.Min();
        public T? GetMax() => _range.Max();
        public virtual T[] RealizeValue(int amount)
        {
            return Random.Shared.GetItems([.. _range], amount);
        }
    }
}