
namespace TheGame.Interfaces
{
    internal interface IGetValue<out T> { T Value { get; } }
    internal interface ISetValue<in T> { T Value { set; } }

    // C (ECS kind)
    internal interface IValue<T> : IGetValue<T>, ISetValue<T>
    {
        new T Value { get; set; }
    }
}