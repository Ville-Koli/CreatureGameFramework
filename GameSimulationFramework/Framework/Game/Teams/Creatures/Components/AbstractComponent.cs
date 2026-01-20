using System.Text;
using Framework.Game.GameEventArgs;

namespace Framework.Game.Teams.Creatures.Components
{    
    public abstract class Component
    {
        public event EventHandler<StatisticValueChangedArgs>? OnValueChanged;
        private ComponentType type;
        private object? value;
        public Component(ComponentType type, object? value)
        {
            this.type = type;
            this.value = value;
        }
        public ComponentType GetComponentType() => type;
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
        public override string ToString()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append($"Component: {type} Value: {value}");
            return stringBuilder.ToString();
        }
    }
}