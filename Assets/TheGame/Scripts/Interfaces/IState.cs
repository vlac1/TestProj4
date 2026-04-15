
namespace TheGame.Interfaces
{
    //TODO cant enter state if already in it
    internal interface IState
    {
        void Began();
        void Ended();
    }
}