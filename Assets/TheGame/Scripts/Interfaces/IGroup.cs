
namespace TheGame.Interfaces
{
    // System.Collections.Generic.IEnumerable<T> kinda sucks
    internal interface IGroup<T> : ICount, IIndexerGet<T>//Arrayish
    {
    }
}