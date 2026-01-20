namespace Framework.Game.Teams.Creatures.Abilities
{
    public class Abilities : Components.Components<Abilities>
    {
        private List<Ability> _abilities = new();

        public Abilities()
        {
            
        }

        public List<Ability> GetAbilities() => _abilities;
    }
}