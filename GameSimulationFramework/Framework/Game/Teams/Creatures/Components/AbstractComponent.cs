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
        private static T Operation<T, T2>(Component a, Component b, T defaultVal, Func<T2, T2, T> operation)
        {
            object? aVal = a.GetValue();
            object? bVal = b.GetValue();

            if(aVal != null && aVal is T2
            && bVal != null 
            && aVal.GetType() == bVal.GetType())
            {
                T2 aComparable = (T2) aVal;
                T2 bComparable = (T2) bVal;
                return operation(aComparable, bComparable);
            }
            return defaultVal;
        }
        public override int GetHashCode()
        {
            if(value != null) return value.GetHashCode();
            return base.GetHashCode();
        }
        public static bool operator==(Component a, Component b)
        {
            if(ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            if(!ReferenceEquals(a, null) && ReferenceEquals(b, null)) return false;
            if(ReferenceEquals(a, null) && !ReferenceEquals(b, null)) return false;

            return Operation<bool, IComparable>(
                a!, b!, 
                defaultVal: false, 
                operation: (a, b) => {return a.CompareTo(b) == 0;}
            );
        }
        public static bool operator!=(Component a, Component b)
        {
            return !(a == b);
        }
        public static bool operator>(Component a, Component b)
        {
            if(ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            if(!ReferenceEquals(a, null) && ReferenceEquals(b, null)) return false;
            if(ReferenceEquals(a, null) && !ReferenceEquals(b, null)) return false;

            return Operation<bool, IComparable>(
                a!, b!, 
                defaultVal: false, 
                operation: (a, b) => {return a.CompareTo(b) > 0;}
            );
        }
        public static bool operator<(Component a, Component b)
        {
            if(ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            if(!ReferenceEquals(a, null) && ReferenceEquals(b, null)) return false;
            if(ReferenceEquals(a, null) && !ReferenceEquals(b, null)) return false;
            
            return Operation<bool, IComparable>(
                a!, b!, 
                defaultVal: false, 
                operation: (a, b) => {return a.CompareTo(b) < 0;}
            );
        }
        public static bool operator>=(Component a, Component b)
        {
            return !(a < b);
        }
        public static bool operator<=(Component a, Component b)
        {
            return !(a > b);
        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append($"Component: {type} Value: {value}");
            return stringBuilder.ToString();
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            return (value, obj) switch
            {
                (IComparable x, IComparable y) => x.GetType().Equals(y.GetType()) ? x.CompareTo(y) == 0 : false,
                (int x, Component<float> y) => x.CompareTo((int) y.GetTypedValue()) == 0,
                (int x, Component<double> y) => x.CompareTo((int) y.GetTypedValue()) == 0, 
                (float x, Component<int> y) => x.CompareTo((float) y.GetTypedValue()) == 0,
                (float x, Component<double> y) => x.CompareTo((float) y.GetTypedValue()) == 0,
                (double x, Component<int> y) => x.CompareTo((double) y.GetTypedValue()) == 0,
                (double x, Component<float> y) => x.CompareTo((double) y.GetTypedValue()) == 0,                
                (object x, Component y) =>  x.GetType().Equals(y.GetValue()?.GetType()) ? x.Equals(y.GetValue()) : false,
                _ => false
            };
        }
    }
}