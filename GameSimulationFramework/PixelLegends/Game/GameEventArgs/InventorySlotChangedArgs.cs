namespace PixelLegends.Game.GameEventArgs
{
    public class InventorySlotChangedArgs<T> : EventArgs
    {
        private T? _previousItem;
        private T? _toItem;
        public InventorySlotChangedArgs(T? previousItem, T? toItem)
        {
            this._previousItem = previousItem;
            this._toItem = toItem;
        }
        public T? GetPreviousItem() => _previousItem;
        public T? GetToItem() => _toItem;
    }
}