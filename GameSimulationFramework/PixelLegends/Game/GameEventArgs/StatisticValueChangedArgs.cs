namespace PixelLegends.Game.GameEventArgs
{
    public class StatisticValueChangedArgs : EventArgs
    {
        private object? _changedTo;
        public StatisticValueChangedArgs(object? changedTo)
        {
            this._changedTo = changedTo;
        }

        public object? GetChangedTo() => _changedTo;
    }
}