using Framework.Game.Interfaces;

namespace Framework.Game.Teams.Creatures.Abilities
{
    public abstract class Ability : Components.Components<Ability>, IAction<Creature>
    {
        public abstract void Action(Source<Creature> from, Source<Creature> to);
        public ActionType GetActionType()
        {
            return ActionType.Ability;
        }
    }
}