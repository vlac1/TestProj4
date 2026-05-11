
namespace TheGame.Interfaces
{
    internal interface IIndexerGet<out T>
    {
        T this[int index] { get; }
    }
}