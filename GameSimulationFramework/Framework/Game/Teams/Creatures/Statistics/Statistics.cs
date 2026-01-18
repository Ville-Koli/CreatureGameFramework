namespace Framework.Game.Teams.Creatures.Statistics
{
    public class Statistics<T> where T : Statistics<T>
    {
        private Dictionary<StatisticType, Statistic> statistics = new();
        public Statistics(params Statistic[] statistic)
        {
            if(statistics == null) return;

            AddStatistics(statistic);
        }
        public Dictionary<StatisticType, Statistic> GetStatistics()
        {
            return statistics;
        }

        public virtual T Clear()
        {
            statistics.Clear();
            return (T) this;
        }

        public int StatisticCount() => statistics.Count;
        public virtual T AddStatistic(Statistic statistic)
        {
            StatisticType type = statistic.GetStatisticType();
            if(!statistics.ContainsKey(type))
                statistics[type] = statistic;
            return (T) this;      
        }
        public virtual T AddStatistics(params Statistic[] statistic)
        {
            foreach(var stat in statistic)
            {
                AddStatistic(stat);
            }
            return (T) this;
        }
        public virtual T UpdateStatistic(Statistic statistic)
        {
            StatisticType type = statistic.GetStatisticType();
            if(statistics.ContainsKey(type))
                statistics[type].SetValue(statistic.GetValue());
            return (T) this;       
        }
        public virtual T RemoveStatistic(Statistic statistic)
        {
            StatisticType type = statistic.GetStatisticType();
            if(statistics.ContainsKey(type))
                statistics.Remove(type);
            return (T) this;          
        }
        public virtual T RemoveStatistic(StatisticType statistic)
        {
            if(statistics.ContainsKey(statistic))
                statistics.Remove(statistic);
            return (T) this;          
        }
        public virtual Statistic<P>? Query<P>(StatisticType type)
        {
            if(statistics.ContainsKey(type))
                return (Statistic<P>) statistics[type];
            return null;             
        }
    }
}