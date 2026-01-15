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
        public T AddStatistic(Statistic statistic)
        {
            StatisticType type = statistic.GetStatisticType();
            if(!statistics.ContainsKey(type))
                statistics[type] = statistic;
            return (T) this;      
        }
        public T AddStatistics(params Statistic[] statistic)
        {
            foreach(var stat in statistic)
            {
                AddStatistic(stat);
            }
            return (T) this;
        }
        public T UpdateStatistic(Statistic statistic)
        {
            StatisticType type = statistic.GetStatisticType();
            if(statistics.ContainsKey(type))
                statistics[type].SetValue(statistic.GetValue());
            return (T) this;       
        }
        public T RemoveStatistic(Statistic statistic)
        {
            StatisticType type = statistic.GetStatisticType();
            if(statistics.ContainsKey(type))
                statistics.Remove(type);
            return (T) this;          
        }
        public Statistic<P>? Query<P>(StatisticType type)
        {
            if(statistics.ContainsKey(type))
                return (Statistic<P>) statistics[type];
            return null;             
        }
    }
}