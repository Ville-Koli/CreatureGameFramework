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

        public void CopyStatistic<P>(Statistic stat, T obj)
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

        public void CopyStatistics(T obj)
        {
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
                        if(
                        type.IsAssignableFrom(value.GetType()))
                        {
                            Type innerType = type.GetGenericArguments()[0];
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