using UnityEngine;

public class InitialState : BaseState
{
    public InitialState(IStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Entering Initial State");
        ServiceLocator.PortalPlacementController.Initialize();
        ServiceLocator.UserController.Initialize();
        stateMachine.SetState<GameState>();
    }

    public override void Exit()
    {
        Debug.Log("Exiting Initial State");
    }
}
