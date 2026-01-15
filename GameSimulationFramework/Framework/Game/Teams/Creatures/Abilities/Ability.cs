using Framework.Game.Interfaces;

namespace Framework.Game.Teams.Creatures.Abilities
{
    public abstract class Ability : Statistics.Statistics<Ability>, IAction
    {
        public abstract void Action(Source from, Source to);
        public ActionType GetActionType()
        {
            return ActionType.Ability;
        }
    }
}