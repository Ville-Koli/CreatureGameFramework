using System.Collections;
using System.Reflection;
using Framework.Game.BaseTypes;
using Framework.Game.GameEventArgs;

namespace Framework.Game.Teams.Creatures.Statistics
{
    public class RandomTemplate<T> : StatisticTemplate<T> where T : Statistics<T>
    {
        public override StatisticTemplate<T> AddStatistic(Statistic statistic)
        {
            if(statistic.GetValue() == null) return this;

            Type[] types = statistic.GetValue()!.GetType().GetGenericArguments();

            if(types.Length == 1)
            {
                Type type = typeof(StatisticRange<>).MakeGenericType(types[0]);
                if (type.IsAssignableFrom(statistic.GetValue()!.GetType()))
                {
                    return base.AddStatistic(statistic);
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

        public override void CopyStatistic<P>(Statistic stat, T obj)
        {
            object? value = stat.GetValue();
            if(value != null && value is StatisticRange<CloneableValue<P>>){
                StatisticRange<CloneableValue<P>> cloneable = (StatisticRange<CloneableValue<P>>?) value!;
                CloneableValue<P>[] realizedValues = cloneable!.RealizeValue(1);
                if(realizedValues!.Length >= 1){
                    CloneableValue<P> cloneableValue = realizedValues[0];
                    obj.AddStatistic(
                        new Statistic<P>(
                            stat.GetStatisticType(), 
                            cloneableValue.Clone())
                        );
                }
            }  
        }

        public override void CopyStatistics(T obj)
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
                    Type? statisticRangeInnerType = GetStatisticRangeInnerType(stat);
                    if(statisticRangeInnerType == null) throw new NotSupportedException("Statistic inner typing does not adhere to StatisticRange!");
                    bool supported = false;
                    foreach(var type in supportedTypes)
                    {
                        // check whether value inherits type
                        if(
                        type.IsAssignableFrom(statisticRangeInnerType)
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
                        $"Unsupported type attempted to be copied in statistics template: {value.GetType()} inner type: {statisticRangeInnerType}"
                    );                            
                        
                }
            }
        }

        private Type? GetStatisticRangeInnerType(Statistic statistic)
        {
            object? value = statistic.GetValue();
            Type[] types = value!.GetType().GetGenericArguments();

            if(types.Length == 1)
            {
                Type statisticRangeInnerType = types[0];
                Type statisticRange = typeof(StatisticRange<>).MakeGenericType(statisticRangeInnerType);
                if (statisticRange.IsAssignableFrom(value!.GetType()))
                {
                    return statisticRangeInnerType;
                }
            }
            return null;
        }
    } 
}