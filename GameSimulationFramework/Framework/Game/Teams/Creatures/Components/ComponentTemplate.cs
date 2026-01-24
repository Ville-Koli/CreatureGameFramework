using System.Reflection;
using Framework.Game.BaseTypes;
using Framework.Game.GameEventArgs;
using Framework.Game.Interfaces;
using Framework.Game.Teams.Creatures.Abilities;

namespace Framework.Game.Teams.Creatures.Components
{
    public class ComponentTemplate<T> : Components<ComponentTemplate<T>> where T : Components<T>
    {
        /**
        Add types here for them to be supported by statistics template
        and make sure that the inner type adheres to ICloneable
        otherwise the function cannot clone it and it will be deemed as unsupported.
        **/
        public static Type[] supportedTypes = {
            typeof(CloneableValue<int>),
            typeof(CloneableValue<float>),  
            typeof(CloneableValue<double>), 
            typeof(CloneableValue<string>),
            typeof(CloneableValue<Ability>)
        };
            
        public ComponentTemplate() 
        {
            
        }
        public virtual List<Component<IFrameworkCloneable>> GetClonableComponents()
        {
            List<Component<IFrameworkCloneable>> cloneables = new();
            foreach(var statistic in GetComponents())
            {
                Component stat = statistic.Value;
                object? value = stat.GetValue();
                if(value != null)
                {
                    if(value != null && value is IFrameworkCloneable)
                    {
                        IFrameworkCloneable cloneable = (IFrameworkCloneable) value;
                        cloneables.Add(new Component<IFrameworkCloneable>(stat.GetComponentType(), cloneable));
                    }
                }
            }
            return cloneables;
        }

        public virtual void CopyComponents(T obj)
        {
            List<Component<IFrameworkCloneable>> cloneables = GetClonableComponents();
            foreach(var cloneable in cloneables)
            {
                IFrameworkCloneable? typedValue = cloneable.GetTypedValue();
                if(typedValue != null)
                {
                    CopyComponent(cloneable.GetComponentType(), typedValue!, obj); 
                }  
            }
        }
        public virtual void CopyComponent(ComponentType type, IFrameworkCloneable cloneable, T obj)
        {
            obj.AddComponent(
                new ObjectComponent(
                    type, 
                    cloneable.ObjectClone()
                    )
            );
        }
        public virtual void CopyComponent(Component component, T obj)
        {
            if(component.GetValue() != null && component.GetValue() is IFrameworkCloneable)
            {
                obj.AddComponent(
                    new ObjectComponent(
                        component.GetComponentType(), 
                        ((IFrameworkCloneable)component.GetValue()!).ObjectClone()
                        )
                );   
            }
        }
    } 
}