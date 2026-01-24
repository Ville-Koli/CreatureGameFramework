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
            }else if (obj != null && obj is double)
            {
                double cloneable = (double) obj;
                return GetValue().CompareTo(cloneable);                
            }
            return -1;
        }
    }
}