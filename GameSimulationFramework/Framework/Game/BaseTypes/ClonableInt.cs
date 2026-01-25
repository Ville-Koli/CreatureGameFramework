namespace Framework.Game.BaseTypes
{
    public class CloneableInt : CloneableValue<int>, IComparable
    {
        public CloneableInt(int value) : base(value)
        {
        }

        public int CompareTo(object? obj)
        {
            if(obj != null && obj is CloneableInt)
            {
                CloneableInt cloneableInt = (CloneableInt) obj;
                return GetValue().CompareTo(cloneableInt.GetValue());
            }
            return obj switch
            {
                int i => GetValue().CompareTo(i),
                float f => ((float) GetValue()).CompareTo(f),
                double d => ((double) GetValue()).CompareTo(d),
                short s => ((short) GetValue()).CompareTo(s),
                long l => ((long) GetValue()).CompareTo(l),
                _ => -1
            };
        }
    }
}