public class InteractionPlayerState : SuperState
{
    // Default child
    protected override State GetDefaultState() { return new UseObjectPlayerState(); }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
}
