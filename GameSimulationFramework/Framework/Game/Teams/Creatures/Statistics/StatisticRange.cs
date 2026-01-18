using System.Collections.Immutable;
using Framework.Game.GameEventArgs;

namespace Framework.Game.Teams.Creatures.Statistics
{
    public class StatisticRange<T>
    {
        private IEnumerable<T> _range;
        public StatisticRange(IEnumerable<T> rangeOfElements)
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