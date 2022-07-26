namespace Scripts.Infrastructure.States
{
    public interface IState : IExitablState
    {
        void Enter();
    }

    public interface IPlaylaodedState<TPlayload> : IExitablState
    {
        void Enter(TPlayload playload);
    }

    public interface IExitablState
    {
        void Exit();
    }
}