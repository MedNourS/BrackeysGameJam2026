public class MovementPlayerState : SuperState
{
    // Default child
    protected override State GetDefaultState() { return new GroundedPlayerState(); }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
}
