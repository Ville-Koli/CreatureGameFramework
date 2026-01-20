using Framework.Game.Teams.Creatures;

namespace Framework.Game.Teams.Bag
{
    public class Bag
    {
        private List<Slot<Creature>> _creatureSlots = new();
        private List<Slot<Item>> _itemSlots = new();

        public Bag(int creatureSlots, int itemSlots)
        {
            for(int i = 0; i < creatureSlots; ++i)
                _creatureSlots.Add(new Slot<Creature>());

            for(int i = 0; i < itemSlots; ++i)
                _itemSlots.Add(new Slot<Item>());
        }

        public List<Slot<Creature>> GetCreatureSlots() => _creatureSlots;
        public List<Slot<Item>> GetItemSlots() => _itemSlots;
        public Slot<Creature>? GetFreeInventorySlot()
        {
            foreach(var slot in _creatureSlots)
            {
                if(slot.Item == null) return slot;
            }
            return null;
        }
    }
}