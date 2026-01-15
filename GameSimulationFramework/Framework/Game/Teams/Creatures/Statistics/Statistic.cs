using Framework.Game.GameEventArgs;

namespace Framework.Game.Teams.Creatures.Statistics
{
    public class Statistic<T> : Statistic
    {
        public Statistic(StatisticType type, T? value) : base(type, value)
        {
            
        }

        public T? GetTypedValue() => (T?) GetValue();
        public void SetTypedValue(T? value) => SetValue(value);
    } 
}