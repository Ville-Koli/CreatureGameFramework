using System.Numerics;
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
        public static bool Comparison(Component a, Component b, Func<IComparable, IComparable, bool> comparison)
        {
            object? aVal = a.GetValue();
            object? bVal = b.GetValue();

            if(aVal != null && aVal is IComparable
            && bVal != null && bVal is IComparable)
            {
                if(aVal.GetType().Equals(bVal.GetType())){
                    IComparable aComparable = (IComparable) aVal;
                    IComparable bComparable = (IComparable) bVal;
                    return comparison(aComparable, bComparable);
                }
            }
            return false;
        }
        public static bool operator>(Component a, Component b)
        {
            return Comparison(a, b, (a, b) => {return a.CompareTo(b) > 0;});
        }
        public static bool operator<(Component a, Component b)
        {
            return Comparison(a, b, (a, b) => {return a.CompareTo(b) < 0;});
        }
        public static bool operator>=(Component a, Component b)
        {
            return Comparison(a, b, (a, b) => {return a.CompareTo(b) >= 0;});
        }
        public static bool operator<=(Component a, Component b)
        {
            return Comparison(a, b, (a, b) => {return a.CompareTo(b) <= 0;});
        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append($"Component: {type} Value: {value}");
            return stringBuilder.ToString();
        }
    }
}