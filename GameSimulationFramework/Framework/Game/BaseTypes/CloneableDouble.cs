namespace Framework.Game.BaseTypes
{
    public class CloneableDouble : CloneableValue<double>
    {
        public CloneableDouble(double value) : base(value)
        {
        }
        public int CompareTo(object? obj)
        {
           if(obj != null && obj is CloneableDouble)
            {
                CloneableDouble cloneable = (CloneableDouble) obj;
                return GetValue().CompareTo(cloneable.GetValue());
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