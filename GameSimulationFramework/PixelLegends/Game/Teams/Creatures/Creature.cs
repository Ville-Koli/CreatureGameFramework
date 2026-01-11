using PixelLegends.Game.Interfaces;

namespace PixelLegends.Game.Teams.Creatures
{
    public class Creature : Statistics.Statistics<Creature>, ISource
    {
        public Source GetSource()
        {
            return new Source(this);
        }
    }
}