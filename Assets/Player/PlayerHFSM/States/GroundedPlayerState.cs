using UnityEngine;
using UnityEngine.InputSystem;

public class GroundedPlayerState : State 
{
    // Constructor for passing context
    public GroundedPlayerState(PlayerContext ctx) : base(ctx) { }

    private InputAction moveAction;
    public override void Enter()
    {
        moveAction = context.playerMap.FindAction("Move");
    }

    public override void Update()
    {
        context.WASDMove(1f, moveAction);

        // If on floor and player jump -> y velocity = Sqrt(-2gh)
        if (context.isGrounded && context.playerMap.FindAction("Jump").triggered)
            context.velocity.y = Mathf.Sqrt(-2 * context.gravity * context.jumpHeight);

        if (!context.isGrounded) { parentSM.ChangeState(new AirbornePlayerState(context)); }
    }
}
