
namespace TheGame.Interfaces
{
    internal interface IState
    {
        void Began();
        void Ended();
    }

    //TODO cant enter state if already in it
    internal interface IState2 : IState
    {
        protected bool _state { get; protected private set; }
        bool IsInState {
            get => _state;
            set
            {
                if(value)
                {
                    if (!_state)
                        Began();
                }else
                {
                    if (_state)
                        Ended();
                }
                _state = value;
            }
        }
    }
}