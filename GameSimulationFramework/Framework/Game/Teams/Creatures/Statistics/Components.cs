namespace Framework.Game.Teams.Creatures.Components
{
    public class Components<T> where T : Components<T>
    {
        private Dictionary<ComponentType, Component> statistics = new();
        public Components(params Component[] statistic)
        {
            if(statistics == null) return;

            AddComponents(statistic);
        }
        public Dictionary<ComponentType, Component> GetComponents()
        {
            return statistics;
        }

        public virtual T Clear()
        {
            statistics.Clear();
            return (T) this;
        }

        public int ComponentCount() => statistics.Count;
        public virtual T AddComponent(Component statistic)
        {
            ComponentType type = statistic.GetComponentType();
            if(!statistics.ContainsKey(type))
                statistics[type] = statistic;
            return (T) this;      
        }
        public virtual T AddComponents(params Component[] statistic)
        {
            foreach(var stat in statistic)
            {
                AddComponent(stat);
            }
            return (T) this;
        }
        public virtual T UpdateStatistic(Component statistic)
        {
            ComponentType type = statistic.GetComponentType();
            if(statistics.ContainsKey(type))
                statistics[type].SetValue(statistic.GetValue());
            return (T) this;       
        }
        public virtual T RemoveStatistic(Component statistic)
        {
            ComponentType type = statistic.GetComponentType();
            if(statistics.ContainsKey(type))
                statistics.Remove(type);
            return (T) this;          
        }
        public virtual T RemoveStatistic(ComponentType statistic)
        {
            if(statistics.ContainsKey(statistic))
                statistics.Remove(statistic);
            return (T) this;          
        }
        public virtual Component<P>? Query<P>(ComponentType type)
        {
            if(statistics.ContainsKey(type))
                return (Component<P>) statistics[type];
            return null;             
        }
    }
}