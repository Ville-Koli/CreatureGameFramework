using Framework.Game.GameEventArgs;

namespace Framework.Game.Teams.Creatures.Statistics
{
    public class StatisticTemplate<T> : Statistics<T> where T : Statistics<T>
    {
        public StatisticTemplate() 
        {
            
        }

        public void CopyStatistics(T obj)
        {
            foreach(var statistic in GetStatistics())
            {
                obj.AddStatistic(obj.Query<>());
            }
        }

    } 
}