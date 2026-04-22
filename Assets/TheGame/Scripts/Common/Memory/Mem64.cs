using System;
using System.Runtime.InteropServices;
using TheGame.Interfaces;

namespace TheGame.Common.Memory
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct Mem64 : IMem
    {
        private fixed byte _mem[64];

        public int Size => 64;
        public IntPtr Ptr => Utils.Ptr(ref this);
    }
}