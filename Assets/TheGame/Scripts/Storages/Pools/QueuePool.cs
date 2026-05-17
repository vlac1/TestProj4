using System;
using System.Collections.Generic;
using TheGame.Interfaces;

namespace TheGame.Storages.Pools
{
    internal class QueuePool<T> : Queue<T>, IStorage<T>, IPrewarm, IDisposable
    {
        private readonly Func<T> _factory; // factory 2x times faster

        public QueuePool(Func<T> factory, int initialCapacity=0) : base(initialCapacity)
        {
            _factory = factory;
            Prewarm(initialCapacity);
        }

        public void Prewarm(int countNew)//Prefabricate
        {
            for (var i = 0; i < countNew; i++)
                Enqueue(_factory());
        }

        public virtual T Request()//Get
            => Count > 0 ? Dequeue() : _factory();

        public virtual void Return(T obj)//Put
            => Enqueue(obj);

        public void Dispose()
            => Clear();
    }
}