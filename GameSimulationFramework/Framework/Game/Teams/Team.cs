using Framework.Game.BaseTypes;
using Framework.Game.Teams.Bag;
using Framework.Game.Teams.Creatures;
using Framework.Game.Teams.Creatures.Components;

namespace Framework.Game.Teams
{
    public class Team
    {
        private List<Slot<Creature>> _creatures = new();
        private Bag.Bag _bag;
        private string _teamName;
        public Team(string name, int teamSlots, int creatureSlots, int itemSlots)
        {
            for(int i = 0; i < teamSlots; ++i)
                _creatures.Add(new Slot<Creature>());

            _bag = new Bag.Bag(creatureSlots, itemSlots);
            _teamName = name;
        }
        public Team(RandomTemplate<Creature> creatureTemplate, string name, int teamSlots, int creatureSlots, int itemSlots) : this(name, teamSlots, creatureSlots, itemSlots)
        {
            for(int i = 0; i < teamSlots; ++i)
            {
                Creature creature = new();
                creatureTemplate.CopyComponents(creature);
                _creatures[i].Item = creature;
            }
        }
        public Bag.Bag GetBag() => _bag;
        public List<Slot<Creature>> GetCreatures() => _creatures;
        public Slot<Creature>? GetFreeCreatureSlot()
        {
            foreach(var slot in _creatures)
            {
                if(slot.Item == null) return slot;
            }
            return null;
        }

        public bool AddCreatureToTeam(Creature creature)
        {
            Slot<Creature>? slot = GetFreeCreatureSlot();
            if(slot != null) { 
                slot.Item = creature; 
                return true; 
            }
            return false;
        }

        public void MoveCreatureToBag(int index)
        {
            if(index < _creatures.Count)
            {
                Slot<Creature>? slot = GetBag().GetFreeInventorySlot();
                if(slot != null)
                {
                    slot!.Item = _creatures[index].Item;
                    _creatures.RemoveAt(index);
                }
            }
        }

        public void MoveCreatureFromBag(int slotIndex, int teamSlotIndex)
        {
            if (teamSlotIndex < _creatures.Count
            && slotIndex < _bag.GetCreatureSlots().Count)
            {
                Slot<Creature> slot = _creatures[teamSlotIndex];
                Slot<Creature> invSlot = _bag.GetCreatureSlots()[slotIndex];
                
                slot.Item = invSlot.Item;
                invSlot.Item = null;
            }
        }

        public void PrintTeam()
        {
            for(int i = 0; i < _creatures.Count; ++i)
            {
                Console.WriteLine($"Team {_teamName} Creature on:\n{_creatures[i].Item}");
            }
        }
    }
}