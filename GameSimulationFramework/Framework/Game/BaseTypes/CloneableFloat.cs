namespace Framework.Game.BaseTypes
{
    public class CloneableFloat : CloneableValue<float>
    {
        public CloneableFloat(float value) : base(value)
        {
        }
        public int CompareTo(object? obj)
        {
            if(obj != null && obj is CloneableFloat)
            {
                CloneableFloat cloneable = (CloneableFloat) obj;
                return GetValue().CompareTo(cloneable.GetValue());
            }else if (obj != null && obj is float)
            {
                float cloneable = (float) obj;
                return GetValue().CompareTo(cloneable);                
            }
            return -1;
        }
    }
}