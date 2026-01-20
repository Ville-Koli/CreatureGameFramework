using Framework.Game.Teams.Creatures.Components;

namespace Framework.Game.Teams.Creatures.Abilities
{
    public class BasicAttack : Ability
    {
        private Component<int> _damageComponent;
        public BasicAttack(int damage)
        {
            _damageComponent = new Component<int>(ComponentType.Damage, damage);
            AddComponent(_damageComponent);
        }

        public override void Action(Source<Creature> from, Source<Creature> to)
        {
            var healthComponent = to.GetSource().Query<int>(ComponentType.Health);

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