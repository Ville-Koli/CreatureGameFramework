using Framework.Game.Interfaces;

namespace Framework.Game.Teams.Creatures.Abilities
{
    public abstract class Ability : Components.Components<Ability>, IAction<Creature>, IFrameworkCloneable<Ability>
    {
        public abstract void Action(Source<Creature> from, Source<Creature> to);

        public abstract Ability Clone();

        public ActionType GetActionType()
        {
            return ActionType.Ability;
        }

        object? Interfaces.IFrameworkCloneable.ObjectClone()
        {
            return Clone();
        }
    }
}