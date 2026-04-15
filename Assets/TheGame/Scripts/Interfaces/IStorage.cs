
namespace TheGame.Interfaces
{
    internal interface IStorage<T>//ISource, Provider
    {
        T Request();
        void Return(T item);
    }
}