public interface IState
{
    void Enter();
    void Exit();
}

public class BaseState : IState
{
    protected readonly IStateMachine stateMachine;

    protected BaseState(IStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
}
