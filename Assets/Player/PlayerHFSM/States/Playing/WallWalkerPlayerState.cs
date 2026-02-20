using UnityEngine;

public class WallWalkerPlayerState : State
{
    // Constructor for passing context
    public WallWalkerPlayerState(PlayerContext ctx) : base(ctx) { }

    public override void Enter()
    {
        Debug.Log("On ground, walking");
    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        // Logic for moving, see spheremovement
        // If grabbed object
        // parentSM.ChangeState(new CapturedPlayerState(context));
    }
}