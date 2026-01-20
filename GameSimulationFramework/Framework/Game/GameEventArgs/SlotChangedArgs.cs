namespace Framework.Game.GameEventArgs
{
    public class SlotChangedArgs<T> : EventArgs
    {
        private T? _previousItem;
        private T? _toItem;
        public SlotChangedArgs(T? previousItem, T? toItem)
        {
            this._previousItem = previousItem;
            this._toItem = toItem;
        }
        public T? GetPreviousItem() => _previousItem;
        public T? GetToItem() => _toItem;
    }
}