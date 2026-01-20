using Framework.Game.GameEventArgs;

namespace Framework.Game.Teams.Creatures.Components
{
    public class Component<T> : Component
    {
        public Component(ComponentType type, T? value) : base(type, value)
        {
            
        }

        public T? GetTypedValue() => (T?) GetValue();
        public void SetTypedValue(T? value) => SetValue(value);
    } 
}