using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class CapturedPlayerState : State
{
    // Constructor for passing context
    public CapturedPlayerState(PlayerContext ctx) : base(ctx) { }
    private float holdTimer;
    private bool isHolding = false;
    private Rigidbody rb;
    private Transform catapultObject;
    private GameObject regularObject;

    private RaycastHit hit;

    public override void Enter()
    {
        Debug.Log("Object captured! Ready to spore!");

        //Update the progress on capturing
        ProgressManager.Instance.UpdateProgress();

        holdTimer = 0;
        context.sporeLoadImage.fillAmount = 0;
        foreach(Transform t in context.capturedObject.transform)
        {
            Debug.Log(t.name);
            if(t.tag == "Catapult" || t.name == "Entrance")
            {
                Debug.Log("Found Catapult");
                catapultObject = t;
            }
        }
        context.body.position = catapultObject.position + new Vector3(0, context.body.GetComponent<MeshFilter>().mesh.bounds.extents.y, 0);

        paintInkAround();

        context.capturedObject.GetComponent<ModelSwitching>().SwapModels();
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

    private void paintInkAround()
    {
        Vector3 size = context.body.transform.GetComponent<MeshFilter>().mesh.bounds.size;
        Vector3 extents = context.body.transform.GetComponent<MeshFilter>().mesh.bounds.extents;

        Ray forwardRay = new Ray(context.body.transform.position, Vector3.forward);
        Ray backRay = new Ray(context.body.transform.position, Vector3.back);
        Ray upRay = new Ray(context.body.transform.position, Vector3.up);
        Ray downRay = new Ray(context.body.transform.position, Vector3.down);
        Ray leftRay = new Ray(context.body.transform.position, Vector3.left);
        Ray rightRay= new Ray(context.body.transform.position, Vector3.right);

        if(Physics.Raycast(forwardRay, out hit, extents.z + context.captureArea, context.terrainMask))
        {
            InkManager.Instance.createInkBlob(hit.point, hit.normal, size.x, size.y);
        }

        if(Physics.Raycast(backRay, out hit, extents.z + context.captureArea, context.terrainMask))
        {
            InkManager.Instance.createInkBlob(hit.point, hit.normal, size.x, size.y);
        }

        if(Physics.Raycast(upRay, out hit, extents.y + context.captureArea, context.terrainMask))
        {
            InkManager.Instance.createInkBlob(hit.point, hit.normal, size.z, size.x);
        }

        if(Physics.Raycast(downRay, out hit, extents.y + context.captureArea, context.terrainMask))
        {
            InkManager.Instance.createInkBlob(hit.point, hit.normal, size.z, size.x);
        }

        if(Physics.Raycast(leftRay, out hit, extents.x + context.captureArea, context.terrainMask))
        {
            InkManager.Instance.createInkBlob(hit.point, hit.normal, size.z, size.y);
        }

        if(Physics.Raycast(rightRay, out hit, extents.x + context.captureArea, context.terrainMask))
        {
            InkManager.Instance.createInkBlob(hit.point, hit.normal, size.z, size.y);
        }
    }
}