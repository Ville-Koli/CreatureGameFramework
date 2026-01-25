using System.Text;
using Framework.Game.Extensions;

namespace Framework.Game.Teams.Creatures.Components
{
    public class Components<T> where T : Components<T>
    {
        private Dictionary<ComponentType, Component> components = new();
        public Components(params Component[] component)
        {
            if(components == null) return;

            AddComponents(component);
        }
        public Dictionary<ComponentType, Component> GetComponents()
        {
            return components;
        }

        public virtual T Clear()
        {
            components.Clear();
            return (T) this;
        }

        public int ComponentCount() => components.Count;
        public virtual T AddComponent(Component component)
        {
            ComponentType type = component.GetComponentType();
            if(!components.ContainsKey(type))
                components[type] = component;
            return (T) this;      
        }
        public virtual T AddComponents(params Component[] component)
        {
            foreach(var stat in component)
            {
                AddComponent(stat);
            }
            return (T) this;
        }
        public virtual T UpdateStatistic(Component component)
        {
            ComponentType type = component.GetComponentType();
            if(components.ContainsKey(type))
                components[type].SetValue(component.GetValue());
            return (T) this;       
        }
        public virtual T RemoveComponent(Component component)
        {
            ComponentType type = component.GetComponentType();
            if(components.ContainsKey(type))
                components.Remove(type);
            return (T) this;          
        }
        public virtual T RemoveComponent(ComponentType component)
        {
            if(components.ContainsKey(component))
                components.Remove(component);
            return (T) this;          
        }
        public virtual Component<P>? Query<P>(ComponentType type)
        {
            if (components.ContainsKey(type))
            {
                Component component = components[type];
                if(component is Component<P>)
                    return (Component<P>) component;
                else if(component is ObjectComponent)
                {
                    RemoveComponent(type);
                    Component<P> typeP = new Component<P>(type, (P?) component.GetValue());
                    AddComponent(typeP);
                    return typeP;
                }
                    
            }
            return null;             
        }
        public virtual List<Component<P>?> Query<P>()
        {
            List<Component<P>?> matches = new();
            foreach(var pair in GetComponents())
            {
                if(pair.Value is Component<P>) matches.Add((Component<P>?)pair.Value);
            }
            return matches;           
        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new();
            var sep = "\n" + "\t".GetExpansion(3);
            foreach(var pair in GetComponents())
            {
                if(pair.Value.GetValue() != null)
                {
                    var ls = pair.Value.GetValue()?.ToString()!.Split("\n")!;
                    string valueString = string.Join(sep, ls);
                    stringBuilder.Append($"Component: {pair.Key} Value: {valueString}\n");
                }
            }
            return stringBuilder.ToString();
        }
    }
}