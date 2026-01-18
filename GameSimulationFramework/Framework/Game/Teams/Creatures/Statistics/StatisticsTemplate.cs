using System.Reflection;
using Framework.Game.BaseTypes;
using Framework.Game.GameEventArgs;

namespace Framework.Game.Teams.Creatures.Statistics
{
    public class StatisticTemplate<T> : Statistics<StatisticTemplate<T>> where T : Statistics<T>
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
            typeof(CloneableValue<string>)
        };
            
        public StatisticTemplate() 
        {
            
        }

        public virtual void CopyStatistic<P>(Statistic stat, T obj)
        {
            object? value = stat.GetValue();
            if(value != null && value is CloneableValue<P>){
                CloneableValue<P> cloneable = (CloneableValue<P>?) stat.GetValue()!;

                obj.AddStatistic(
                    new Statistic<P>(
                        stat.GetStatisticType(), 
                        cloneable.Clone())
                    );
            }            
        }

        public virtual void CopyStatistics(T obj)
        {
            // get the copy statistic method earlier so we only need to generate the
            // generic method in the for loop
            Type thisType = GetType();
            MethodInfo methodInfo = thisType.GetMethod("CopyStatistic")!;
            foreach(var statistic in GetStatistics())
            {
                Statistic stat = statistic.Value;
                object? value = stat.GetValue();
                if(value != null)
                {
                    bool supported = false;
                    foreach(var type in supportedTypes)
                    {
                        // check whether value inherits type
                        if(
                        type.IsAssignableFrom(value.GetType())
                        )
                        {
                            // get generic arguments to get the inner typing of a cloneable value
                            Type[] innerTypes = type.GetGenericArguments();

                            if(innerTypes.Length > 1 || innerTypes.Length == 0) throw new NotSupportedException("Unsupported amount of generic arguments in type!");
                            Type innerType = innerTypes[0];

                            // generate the generic method of copy statistic and invoke it with the statistic element and the object
                            // in which we want to copy the statistic element to it
                            MethodInfo generic = methodInfo!.MakeGenericMethod(innerType);
                            generic?.Invoke(this, [stat, obj]);
                            supported = true;
                            break;
                        }
                    }

                    if(!supported) throw new NotSupportedException(
                        $"Unsupported type attempted to be copied in statistics template: {value.GetType()}"
                    );
                }
            }
        }
    } 
}