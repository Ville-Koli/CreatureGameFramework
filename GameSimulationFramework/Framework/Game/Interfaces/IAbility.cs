namespace Framework.Game.Interfaces
{
    public interface IAbility
    {
        public float GetRawDamage();
        public void SetRawDamage(float damage);
        public float GetStaminaCost();
        public void SetStaminaCost(float staminaCost);
    }
}