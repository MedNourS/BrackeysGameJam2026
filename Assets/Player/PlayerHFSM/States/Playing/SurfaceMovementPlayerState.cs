using System;
using UnityEngine;

public class SurfaceMovementPlayerState : State
{
    private TentacleManagingState tentacleManagerState;

    [SerializeField] private Transform sphere;

    private InputSystem_Actions controls;

    private Vector3 forward;
    private Vector3 dir;
    private Vector3 up;

    private float inputAngle = 0;

    // Constructor for passing context
    public SurfaceMovementPlayerState(PlayerContext ctx) : base(ctx) { }

    public override void Enter()
    {
        controls = new InputSystem_Actions();
        Cursor.lockState = CursorLockMode.Locked;
        controls.Player.Move.Enable();

        forward = context.body.forward;
        for (int i = 1; i < 5; i++)
        {
            if (SnapToNearestSurface(Mathf.Pow(10f, i))) { break; }
        }

        /* Run another state in parallel */
        tentacleManagerState = new TentacleManagingState(context);
        tentacleManagerState.Enter();
    }

    public override void Exit()
    {
        controls.Player.Move.Disable();

        /* Run another state in parallel */
        tentacleManagerState.Exit();
    }

    public override void Update()
    {
        if (context.isCapturing)
        {
            parentSM.ChangeState(new CapturedPlayerState(context));
            return;
        }

        Vector2 input = controls.Player.Move.ReadValue<Vector2>();
        float cameraYaw = context.cam.transform.eulerAngles.y;
        float degreesToRadians = -(float)Math.PI / 180f;

        input = new Vector2(
            input.x * (float)Math.Cos(cameraYaw * degreesToRadians) - input.y * (float)Math.Sin(cameraYaw * degreesToRadians),
            input.x * (float)Math.Sin(cameraYaw * degreesToRadians) + input.y * (float)Math.Cos(cameraYaw * degreesToRadians)
        );

        Vector2 normalizedInput = input.normalized;
        inputAngle = (input != Vector2.zero)
            ? Mathf.Atan2(normalizedInput.x, normalizedInput.y) * Mathf.Rad2Deg
            : 0f;

        dir = Quaternion.AngleAxis(inputAngle, up) * forward;
        Vector3 move = dir * context.movementSpeed * input.magnitude * Time.deltaTime;
        Vector3 ahead = context.body.position + move + up * 0.001f;
        context.body.position = ahead;

        SnapToNearestSurface(2f);
        float dist = 0f;
        while (dist < 0.25f)
        {
            FindClosestPoint(context.body.position, 0.5f, out _, out Vector3? newClosest, out dist, out _);
            if (dist < 0.25f) { break; }

            Vector3 offset = newClosest.Value - context.body.position;
            float xOff = Vector3.Dot(offset, dir);
            float yOff = Vector3.Dot(offset, up);
            float shiftDist = Mathf.Sqrt(0.25f - yOff * yOff) + xOff;

            context.body.position += shiftDist * dir;
            SnapToNearestSurface(2f);
        }
        FindClosestPoint(context.body.position, 10000f, out _, out Vector3? closestDebug, out _, out _);

        /* Run another state in parallel */
        tentacleManagerState.Update();
    }


    /* SphereMovement.cs functions (thank you Declan!) */
    void FindClosestPoint(Vector3 origin, float radius, out Collider[] hits, out Vector3? closestPoint, out float closestDist, out Vector3? surfaceNormal)
    {
        hits = Physics.OverlapSphere(origin, radius, context.terrainMask);

        closestPoint = null;
        surfaceNormal = null;

        closestDist = float.MaxValue;

        foreach (Collider collider in hits)
        {
            Vector3 currPoint = collider.ClosestPoint(origin);
            float distSqr = (currPoint - origin).sqrMagnitude;

            if (distSqr < closestDist)
            {
                closestPoint = currPoint;
                closestDist = distSqr;
            }
        }

        if (closestPoint != null)
        {
            surfaceNormal = Vector3.Normalize(origin - closestPoint.Value);
        }
    }

    bool SnapToNearestSurface(float range)
    {
        FindClosestPoint(context.body.position, range, out _, out Vector3? newPoint, out _, out Vector3? newUp);
        if (newPoint != null)
        {
            up = newUp.Value;
            context.body.position = newPoint.Value + up * 0.5f;
            forward = Vector3.ProjectOnPlane(forward, up).normalized;
            dir = Quaternion.AngleAxis(inputAngle, up) * forward;

            // sphere.rotation = Quaternion.LookRotation(forward, up);
        }
        return newPoint != null;
    }
}