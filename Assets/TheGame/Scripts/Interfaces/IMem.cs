using System;

namespace TheGame.Interfaces
{
    public interface IMem
    {
        int Size { get; }
        IntPtr Ptr { get; }
    }
}