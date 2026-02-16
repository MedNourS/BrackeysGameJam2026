public class MovementPlayerState : SuperState
{
    // Constructor for passing context
    public MovementPlayerState(PlayerContext ctx) : base(ctx) { }

    // Default child
    protected override State GetDefaultState() { return new GroundedPlayerState(context); }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
}