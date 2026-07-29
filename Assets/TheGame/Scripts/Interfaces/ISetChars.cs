using System;

namespace TheGame.Interfaces
{
    internal interface ISetChars
    {
        public void SetChars(ReadOnlySpan<char> chars);
    }
}