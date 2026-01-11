namespace PixelLegends.Game
{
    public class Source
    {
        private object source;
        public Source(object source)
        {
            this.source = source;
        }
        public object GetSource() => source;
    }
}