using PixelLegends.Game.GameEventArgs;

namespace PixelLegends.Game.Teams.Bag
{
    public class InventorySlot<T>
    {
        public event EventHandler<InventorySlotChangedArgs<T>>? OnItemChanged;
        public T? Item { 
            get {
                return item;
            } 
            set {
                T? previousItem = item;
                item = value;
                OnItemChangedEvent(previousItem, item);
            }}
        private T? item;
        
        public InventorySlot()
        {
            
        }
        protected virtual void OnItemChangedEvent(T? previousItem, T? toItem)
        {
            OnItemChanged?.Invoke(this, new InventorySlotChangedArgs<T>(previousItem, toItem));
        }
    }
}