using System.Collections;
using Framework.Game.Interfaces;

namespace Framework.Game.BaseTypes
{
    public class CloneableValueRange : IEnumerable<CloneableFloat>, IClonable<CloneableFloat[]>
    {
        private CloneableFloat[] _value;
        private float _min;
        private float _max;
        private float _step;
        public CloneableValueRange(float min, float max, float step)
        {
            _min = min;
            _max = max;
            _step = step;

            int steps = (int)((max - min) / step) + 1;

            _value = new CloneableFloat[steps];

            for(int i = 0; i < steps; i++)
            {
                _value[i] = new CloneableFloat(min + step * i);
            }
        }

        public int Length() => _value.Length;

        public IEnumerator<CloneableFloat> GetEnumerator()
        {
            return new CloneableValueRangeEnumerator(_value);
        }

        public CloneableFloat[] GetValue() => _value;

        CloneableFloat[] IClonable<CloneableFloat[]>.Clone()
        {
            int steps = (int)((_max - _min) / _step);

            CloneableFloat[] newArray = new CloneableFloat[steps];

            for(int i = 0; i < steps; i++)
            {
                newArray[i] = new CloneableFloat(_min + steps * i);
            }
            return _value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class CloneableValueRangeEnumerator : IEnumerator<CloneableFloat>
    {
        private CloneableFloat[] cloneableRange;
        public CloneableValueRangeEnumerator(CloneableFloat[] range)
        {
            this.cloneableRange = range;
        }
        public CloneableFloat Current {
            get
            {
                try{
                    return cloneableRange[position];
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
            return position < cloneableRange.Length;
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