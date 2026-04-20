using System;
using System.Collections.Generic;
using TheGame.Interfaces;

namespace TheGame.Storages.Pools
{
    // keeps only inactive objects
    internal class StackPool<T> : Stack<T>, IStorage<T>, IDisposable
    {
        private readonly Func<T> _factory; // factory 2x times faster than new T()

        public StackPool(int initialCapacity, Func<T> factory) : base(initialCapacity)
        {
            _factory = factory;
            for (var i = 0; i < initialCapacity; i++)
                Push(factory());
        }

        public T Request()//old Get
            => Count > 0 ? Pop() : _factory();

        public void Return(T obj)//old Put
            => Push(obj);

        public void Dispose()
            => Clear();
    }
}