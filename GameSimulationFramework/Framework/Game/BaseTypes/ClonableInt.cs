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
            }else if (obj != null && obj is int)
            {
                int cloneableInt = (int) obj;
                return GetValue().CompareTo(cloneableInt);                
            }
            return -1;
        }
    }
}