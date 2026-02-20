using UnityEngine;

public class CapturedPlayerState : State
{
    // Constructor for passing context
    public CapturedPlayerState(PlayerContext ctx) : base(ctx) { }

    public override void Enter()
    {
        Debug.Log("Object captured! Ready to spore!");
    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        // Logic for looking around, if fire spore,
        //parentSM.ChangeState(new AirbornePlayerState(context));
    }
}