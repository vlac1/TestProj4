using System;
using System.Runtime.InteropServices;
using TheGame.Interfaces;

namespace TheGame.Common.Memory
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MemView<V, M>
        where V : unmanaged
        where M : unmanaged, IMem
    {
        private M _mem;// DONT MAKE readonly
        // order IMPORTANT, this after _mem
        private readonly int _count;
        public int Count => _count;

        public MemView(int count)
        {
            _mem = default;
            _count = count;
        }

        public V this[int index]
        {
            get => *GetAtIndex(index);
            set => *GetAtIndex(index) = value;
        }

        private V* GetAtIndex(int index)
        {
            if (index >= Count)
            {
                var msg = string.Format("MemView cant read at {0}", index);
                throw new ArgumentOutOfRangeException(msg);
            }

            var start = (V*)_mem.Ptr;//or (V*)&this
            return start + index;// faster than &start[index]
        }
    }
}