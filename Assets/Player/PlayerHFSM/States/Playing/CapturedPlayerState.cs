using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class CapturedPlayerState : State
{
    // Constructor for passing context
    public CapturedPlayerState(PlayerContext ctx) : base(ctx) { }
    private float holdTimer;
    private bool isHolding = false;
    private Rigidbody rb;

    public override void Enter()
    {
        Debug.Log("Object captured! Ready to spore!");
        holdTimer = 0;
        context.sporeLoadImage.fillAmount = 0;
        context.body.position = context.catapultObject.transform.position;
    }

    public override void Exit()
    {
        //Just to be safe
        holdTimer = 0;
        context.sporeLoadImage.fillAmount = 0;
    }

    public override void Update()
    {
        // Logic for looking around, if fire spore,
        //parentSM.ChangeState(new AirbornePlayerState(context));

        //Logic for looking around
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            isHolding = true;
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            isHolding = false;
            
            //Add the rigidbody
            rb = context.body.gameObject.AddComponent<Rigidbody>();
            Debug.Log("Release");

            rb.AddForce(context.direction.forward * context.catapultForce * holdTimer, ForceMode.Impulse);
            holdTimer = 0;
            context.sporeLoadImage.fillAmount = 0;
            parentSM.ChangeState(new AirbornePlayerState(context));
            return;
        }

        if (isHolding && holdTimer <= context.catapultMaxTimeHold)
        {
            float progress = holdTimer / context.catapultMaxTimeHold;


            holdTimer += Time.deltaTime;
            context.sporeLoadImage.fillAmount = progress;
            context.sporeLoadImage.color = Color.Lerp(context.loadEndingColor, context.loadStartingColor, progress);
        }
        if(holdTimer >= context.catapultMaxTimeHold)
        {
            holdTimer = context.catapultMaxTimeHold;
        }
    }
}