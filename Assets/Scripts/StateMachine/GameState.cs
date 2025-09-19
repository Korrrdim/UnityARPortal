using UnityEngine;

public class GameState : BaseState
{
    public GameState(IStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Entering Game State");
    }

    public override void Exit()
    {
        ServiceLocator.PortalPlacementController.Dispose();
        ServiceLocator.UserController.Dispose();
        Debug.Log("Exiting Game State");
    }
}
