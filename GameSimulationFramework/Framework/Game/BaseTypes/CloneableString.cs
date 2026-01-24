namespace Framework.Game.BaseTypes
{
    public class CloneableString : CloneableValue<string>
    {
        public CloneableString(string value) : base(value)
        {
        }

        public override string Clone()
        {
            string clonedString = "";
            foreach(var c in GetValue())
            {
                clonedString += c;
            }
            return clonedString;
        }
        public int CompareTo(object? obj)
        {
            if(obj != null && obj is CloneableString)
            {
                CloneableString cloneable = (CloneableString) obj;
                return GetValue().CompareTo(cloneable.GetValue());
            }else if (obj != null && obj is string)
            {
                string cloneable = (string) obj;
                return GetValue().CompareTo(cloneable);                
            }
            return -1;
        }
    }
}