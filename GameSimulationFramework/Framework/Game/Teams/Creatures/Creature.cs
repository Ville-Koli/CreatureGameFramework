using System.Text;
using Framework.Game.Interfaces;

namespace Framework.Game.Teams.Creatures
{
    public class Creature : Components.Components<Creature>, ISource<Creature>
    {
        public Source<Creature> GetSource()
        {
            return new Source<Creature>(this);
        }
    }
}