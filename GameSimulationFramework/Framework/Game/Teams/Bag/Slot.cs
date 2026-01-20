using Framework.Game.GameEventArgs;

namespace Framework.Game.Teams.Bag
{
    public class Slot<T>
    {
        public event EventHandler<SlotChangedArgs<T>>? OnItemChanged;
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
        
        public Slot()
        {
            
        }
        protected virtual void OnItemChangedEvent(T? previousItem, T? toItem)
        {
            OnItemChanged?.Invoke(this, new SlotChangedArgs<T>(previousItem, toItem));
        }
    }
}