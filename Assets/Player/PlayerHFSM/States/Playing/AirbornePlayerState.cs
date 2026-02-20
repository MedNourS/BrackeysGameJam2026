using UnityEngine;

public class AirbornePlayerState : State
{
    // Constructor for passing context
    public AirbornePlayerState(PlayerContext ctx) : base(ctx) { }

    public override void Enter()
    {
        Debug.Log("Object gone airborne");
    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        // Logic for looking around, on land do:
        //parentSM.ChangeState(new WallWalkerPlayerState(context));
    }
}