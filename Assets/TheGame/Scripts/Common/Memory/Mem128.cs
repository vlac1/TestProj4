using System;
using System.Runtime.InteropServices;
using TheGame.Interfaces;

namespace TheGame.Common.Memory
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct Mem128 : IMem
    {
        private fixed byte _mem[128];

        public int Size => 128;
        public IntPtr Ptr
        {
            get
            {
                fixed (void* ptr = &this)
                    return (IntPtr)ptr;
            }
        }
    }
}