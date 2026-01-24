using System.Collections;
using Framework.Game.Interfaces;

namespace Framework.Game.BaseTypes
{
    /**
    IEnumerable structure, which generates a range of cloneable float values between
    the range of min and max with specified step size, but does not save the results into memory
    rather evaluates the desired element during runtime.
    **/
    public class CloneableLazyValueRange : IEnumerable<CloneableFloat>, IFrameworkCloneable<CloneableLazyValueRange>, IComparable
    {
        private float _min;
        private float _max;
        private float _step;
        private int _steps;
        public CloneableLazyValueRange(float min, float max, float step)
        {
            if(_max < _min) throw new InvalidOperationException("min cannot be larger than max!");
            _min = min;
            _max = max;
            _step = step > 0 ? step : -step;
            _steps = (int)((_max - _min) / _step) + 1;
        }

        public int Length() => _steps;

        public IEnumerator<CloneableFloat> GetEnumerator()
        {
            return new CloneableLazyValueRangeEnumerator(_min, _max, _step);
        }

        public CloneableFloat GetValue(int index) => new CloneableFloat(Math.Clamp(_min + index * _step, _min, _max));

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public CloneableLazyValueRange Clone()
        {
            return new CloneableLazyValueRange(_min, _max, _step);
        }

        public object? ObjectClone()
        {
            return Clone();
        }

        public int CompareTo(object? obj)
        {
            if(obj != null && obj is CloneableLazyValueRange)
            {
                CloneableLazyValueRange range = (CloneableLazyValueRange) obj;
                if(range._min == _min && range._max == _max) return 0;
                if(range._min <= _min && range._max > _max) return -1;
                if(range._min >= _min && range._max < _max) return 1;
                if(range._min < _min && range._max < _max) return 1;
                if(range._max < _min) return 1;
                if(range._min > _max) return -1;
                if(range._min < _max && _max <= range._max && _min < range._min) return -1;
                if(range._min < _min && _min < range._max && _max >= range._max) return 1;
                
            }
            return -1;
        }

        public override string ToString()
        {
            return $"[{_min}, {_max}]";
        }
    }

    public class CloneableLazyValueRangeEnumerator : IEnumerator<CloneableFloat>
    {
        private float _min;
        private float _max;
        private float _step;
        private int _steps;
        public CloneableLazyValueRangeEnumerator(float min, float max, float step)
        {
            if(_max < _min) throw new InvalidOperationException("min cannot be larger than max!");
            _min = min;
            _max = max;
            _step = step > 0 ? step : -step;
            _steps = (int)((_max - _min) / _step) + 1;
        }
        public CloneableFloat Current {
            get
            {
                try{
                    if(position > _steps) throw new InvalidOperationException();
                    else return new CloneableFloat(_min + position * _step);
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        object IEnumerator.Current => Current;

        public int position = -1;

        public bool MoveNext()
        {
            position++;
            return position < _steps;
        }

        public void Reset()
        {
            position = -1;
        }

        public void Dispose()
        {
            // do nothing as there are no manual memory allocations and let
            // garbage collection do the rest
        }
    }
}