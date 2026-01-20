using Framework.Game.BaseTypes;

namespace Framework.Game.Extensions
{
    public static class Extensions
    {
        public static CloneableInt ToCloneable(this int i) => new CloneableInt(i);
        public static CloneableFloat ToCloneable(this float i) => new CloneableFloat(i);
        public static CloneableDouble ToCloneable(this double i) => new CloneableDouble(i);
        public static CloneableString ToCloneable(this string i) => new CloneableString(i);
        public static string GetExpansion(this string str, int p) {
            string expanded = "";
            for(int i = 0; i < p; ++i)
            {
                expanded += str;
            }
            return expanded;
        }
    }
}