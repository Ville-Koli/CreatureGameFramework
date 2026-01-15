using Framework.Game.Interfaces;

namespace Framework.Game.Teams.Creatures
{
    public class Creature : Statistics.Statistics<Creature>, ISource
    {
        public Source GetSource()
        {
            return new Source(this);
        }
    }
}