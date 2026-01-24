using System.Collections;
using System.Reflection;
using Framework.Game.BaseTypes;
using Framework.Game.GameEventArgs;
using Framework.Game.Interfaces;

namespace Framework.Game.Teams.Creatures.Components
{
    public class RandomTemplate<T> : ComponentTemplate<T> where T : Components<T>
    {
        public override ComponentTemplate<T> AddComponent(Component statistic)
        {
            if(statistic.GetValue() == null) return this;

            Type[] types = statistic.GetValue()!.GetType().GetGenericArguments();

            if(types.Length == 1)
            {
                Type type = typeof(ComponentRange<>).MakeGenericType(types[0]);
                if (type.IsAssignableFrom(statistic.GetValue()!.GetType()))
                {
                    return base.AddComponent(statistic);
                }
                else
                {
                    throw new NotSupportedException(
                        "Unsupported typing as statistics value's type does not derive from statistic range or it isn't of type statistic range!"
                        );
                }
            }
            else
            {
                throw new NotSupportedException("Unsupported amount of generic arguments (==0) (>1)!");
            }
        }

        public ComponentTemplate<T> RealizeTemplate()
        {
            ComponentTemplate<T> template = new();
            List<Component<IFrameworkCloneable>> cloneables = GetClonableComponents();
            foreach(var component in cloneables)
            {
                template.AddComponent(component);
            }
            return template;
        }
        public override List<Component<IFrameworkCloneable>> GetClonableComponents()
        {
            List<Component<IFrameworkCloneable>> cloneables = new();
            foreach(var statistic in GetComponents())
            {
                Component stat = statistic.Value;
                object? value = stat.GetValue();
                if(value != null)
                {
                    if (value is IRealize)
                    {
                        IRealize realizeableValue = (IRealize) value;
                        object?[]? realizedValues = realizeableValue.RealizeValue(1);
                        if(realizedValues != null)
                        {
                            foreach(var elem in realizedValues)
                            {
                                if(elem != null && elem is IFrameworkCloneable)
                                {
                                    IFrameworkCloneable cloneable = (IFrameworkCloneable) elem;
                                    cloneables.Add(new Component<IFrameworkCloneable>(stat.GetComponentType(), cloneable));
                                }
                            }
                        }
                    }
                }
            }
            return cloneables;
        }    
        public override void CopyComponents(T obj)
        {
            foreach(var statistic in GetComponents())
            {
                Component stat = statistic.Value;
                object? value = stat.GetValue();
                if(value != null)
                {
                    if (value is IRealize)
                    {
                        IRealize realizeableValue = (IRealize) value;
                        object?[]? realizedValues = realizeableValue.RealizeValue(1);
                        if(realizedValues != null)
                        {
                            foreach(var elem in realizedValues)
                            {
                                if(elem is IFrameworkCloneable)
                                {
                                    IFrameworkCloneable cloneable = (IFrameworkCloneable) elem;
                                    CopyComponent(stat.GetComponentType(), cloneable, obj);
                                }
                            }
                        }
                    }
                }
            }
        }
    } 
}