public class InteractionPlayerState : SuperState
{
    // Constructor for passing context
    public InteractionPlayerState(PlayerContext ctx) : base(ctx) { }

    // Default child
    protected override State GetDefaultState() { return new UseObjectPlayerState(context); }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
}
