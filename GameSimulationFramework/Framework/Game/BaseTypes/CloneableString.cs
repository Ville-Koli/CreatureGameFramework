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
    }
}