using PixelLegends.Game.GameEventArgs;

namespace PixelLegends.Game.Teams.Creatures.Statistics
{    
    public abstract class Statistic
    {
        public event EventHandler<StatisticValueChangedArgs>? OnValueChanged;
        private StatisticType type;
        private object? value;
        public Statistic(StatisticType type, object? value)
        {
            this.type = type;
            this.value = value;
        }
        public StatisticType GetStatisticType() => type;
        public void SetStatisticType(StatisticType newType) => type = newType;
        public object? GetValue() => value;
        public void SetValue(object? newValue)
        {
            this.value = newValue;
            OnValueChangedEvent(newValue);
        }
        protected private void OnValueChangedEvent(object? value)
        {
            OnValueChanged?.Invoke(this, new StatisticValueChangedArgs(value));
        }
    }
}