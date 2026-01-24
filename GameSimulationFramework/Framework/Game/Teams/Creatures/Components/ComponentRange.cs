using System.Collections.Immutable;
using Framework.Game.GameEventArgs;
using Framework.Game.Interfaces;

namespace Framework.Game.Teams.Creatures.Components
{
    public class ComponentRange<T> : IRealize<T>
    {
        private IEnumerable<T> _range;
        public ComponentRange(IEnumerable<T> rangeOfElements)
        {
            _range = rangeOfElements;
        }
        public T? GetMin() => _range.Min();
        public T? GetMax() => _range.Max();
        public int? GetCount() => _range.Count();
        public virtual T?[]? RealizeValue(int amount)
        {
            if(_range.Count() > 0)
                return Random.Shared.GetItems([.. _range], amount);
            return null;
        }

        object?[]? IRealize.RealizeValue(int amount)
        {
            T?[]? arr = RealizeValue(amount);
            if(arr != null){
                object?[]? objects = new object[arr.Length];
                for(int i = 0; i < objects.Length; ++i)
                {
                    objects[i] = arr[i];
                }
                return objects;
            }
            return null;
        }
    }
}