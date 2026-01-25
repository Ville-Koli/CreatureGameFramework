using Framework.Game.Teams.Creatures.Components;

namespace Framework.Game.Teams.Creatures.Abilities
{
    public class BasicAttack : Ability
    {
        private Component<float> _damageComponent;
        public BasicAttack(float damage)
        {
            _damageComponent = new Component<float>(ComponentType.Damage, damage);
            AddComponent(_damageComponent);
        }

        public override void Action(Source<Creature> from, Source<Creature> to)
        {
            Component<float>? healthComponent = to.GetSource().Query<float>(ComponentType.Health);

            if(healthComponent != null)
            {
                healthComponent.SetTypedValue(healthComponent.GetTypedValue() - _damageComponent.GetTypedValue());
            }
        }

        public override Ability Clone()
        {
            return new BasicAttack(_damageComponent.GetTypedValue());
        }
    }
}