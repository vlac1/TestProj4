using System;
using System.Collections.Generic;
using TheGame.Interfaces;

namespace TheGame.Storages.Pools
{
    // TODO tests
    public class PoolWithActives<TItem> : IStorage<TItem>
        where TItem : IId<int>
    {
        public ushort PoolPos { get; protected set; } // first inactive Object
        // active objects in beginning, inactive at the end
        private readonly List<TItem> _items;
        private readonly Func<TItem> _factory;

        public PoolWithActives(int initialCapacity, Func<TItem> factory)// PreWarm
        {
            _factory = factory;
            _items = new(initialCapacity);
            for (var i = 0; i < initialCapacity; i++)
                _items.Add(_factory());
        }

        public TItem Request()//Get
        {
            var itemInUse = _items[PoolPos];
            itemInUse.Id = PoolPos++;
            if (PoolPos == _items.Count)
            {
                _items.Add(_factory());
            }
            return itemInUse;
        }

        public void Return(TItem usedItem)//Put
        {
            var lastActiveId = --PoolPos;

            // swap usedItem with lastActive
            var lastActive = _items[lastActiveId];
            lastActive.Id = usedItem.Id;
            _items[lastActive.Id] = lastActive;
            _items[lastActiveId] = usedItem;
        }

        public IEnumerable<TItem> Actives()
        {
            while (PoolPos > 0)
            {
                var lastActive = _items[--PoolPos];
                yield return lastActive;
            }
        }
    }
}