using Framework.Game.Teams.Creatures.Components;

namespace Framework.Game.Rounds
{
    public class RoundOrder
    {
        private List<ComponentType> orderBy;
        public List<T> GenerateOrder<T>(List<T> elements) where T : Components<T>
        {
            List<T> order = new();

            // TODO: generate order

            return order;
        }
    }
}