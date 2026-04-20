using System;
using System.Collections.Generic;
using TheGame.Interfaces;

namespace TheGame.Storages.Pools
{
    internal class QueuePool<T> : Queue<T>, IStorage<T>, IDisposable
    {
        private readonly Func<T> _factory; // factory 2x times faster

        public QueuePool(int initialCapacity, Func<T> factory) : base(initialCapacity)
        {
            _factory = factory;
            for (var i = 0; i < initialCapacity; i++)//Prefabricate
                Enqueue(factory());
        }

        public virtual T Request()//Get
            => Count > 0 ? Dequeue() : _factory();

        public virtual void Return(T obj)//Put
            => Enqueue(obj);

        public void Dispose()
            => Clear();
    }
}