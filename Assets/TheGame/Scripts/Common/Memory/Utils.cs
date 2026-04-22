using System;

namespace TheGame.Common.Memory
{
    public static class Utils
    {
        public unsafe static IntPtr Ptr<T>(ref T obj)
            where T : unmanaged
        {
            fixed (void* ptr = &obj)
                return (IntPtr)ptr;
        }
    }
}