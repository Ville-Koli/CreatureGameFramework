using Framework.Game.Interfaces;

namespace Framework.Game.BaseTypes
{
    public class CloneableValue<T> : IFrameworkCloneable<T>
    {
        private T _value;
        public CloneableValue(T value)
        {
            this._value = value;
        }
        
        public T GetValue() => _value;

        public void SetValue(T value) => _value = value;

        public virtual T Clone()
        {
            return _value;
        }
        public override string ToString()
        {
            if(_value != null)
                return _value.ToString()!;
            return "";
        }

        public object? ObjectClone()
        {
            return Clone();
        }
    }
}