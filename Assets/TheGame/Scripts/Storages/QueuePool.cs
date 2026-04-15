using System;
using System.Collections.Generic;

namespace TheGame.Storages
{
    public class QueuePool<T> : Queue<T>, IDisposable
    {
        private Func<T> _factory; // factory 2x times faster

        public QueuePool() { }

        public QueuePool(int initialCapacity, Func<T> factory) : base(initialCapacity)
        {
            Prefabricate(initialCapacity, factory);
        }

        public void Prefabricate(int initialCapacity, Func<T> factory)
        {
            _factory = factory;
            for (var i = 0; i < initialCapacity; i++)
                Enqueue(factory());
        }

        public virtual T Get()
            => Count > 0 ? Dequeue() : _factory();

        public virtual void Put(T obj)
            => Enqueue(obj);

        public void Dispose()
            => Clear();
    }
}