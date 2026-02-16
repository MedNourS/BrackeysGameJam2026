using UnityEngine.InputSystem;

public class AirbornePlayerState : State
{
    // Constructor for passing context
    public AirbornePlayerState(PlayerContext ctx) : base(ctx) { }

    private InputAction moveAction;
    public override void Enter()
    {
        moveAction = context.playerMap.FindAction("Move");
    }

    public override void Update()
    {
        // Walk around in air and thats about it
        context.WASDMove(1f, moveAction);
    }
}