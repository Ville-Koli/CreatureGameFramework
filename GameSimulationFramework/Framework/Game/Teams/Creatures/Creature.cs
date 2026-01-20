using Framework.Game.Interfaces;

namespace Framework.Game.Teams.Creatures
{
    public class Creature : Components.Components<Creature>, ISource
    {
        public Source GetSource()
        {
            return new Source(this);
        }
    }
}