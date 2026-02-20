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
        MonoBehaviour.Destroy(context.body.gameObject.GetComponent<Rigidbody>());
    }

    public override void Update()
    {
        // Logic for looking around, on land do:
        //parentSM.ChangeState(new WallWalkerPlayerState(context));

        Collider[] hits = Physics.OverlapSphere(context.body.position, 0.5f, context.terrainMask);
        if(hits.Length > 0) parentSM.ChangeState(new WallWalkerPlayerState(context));
    }
}