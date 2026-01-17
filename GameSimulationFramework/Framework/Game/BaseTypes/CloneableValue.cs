using Framework.Game.Interfaces;

namespace Framework.Game.BaseTypes
{
    public class CloneableValue<T> : IClonable<T>
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
    }
}