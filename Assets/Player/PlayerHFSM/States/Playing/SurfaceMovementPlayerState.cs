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

        /* Part of Declan's SphereMovement.cs */
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
        /* Easy little code that changes state if the player is capturing */
        if (context.isCapturing)
        {
            parentSM.ChangeState(new CapturedPlayerState(context));
            return;
        }

        /* A deviation of Declan's SphereMovement.cs, where the input is rotated depending on the yaw (left and right angle) of the camera */
        Vector2 input = controls.Player.Move.ReadValue<Vector2>();
        float cameraYaw = context.cam.transform.eulerAngles.y;
        input = new Vector2(
            input.x * Mathf.Cos(cameraYaw * Mathf.Deg2Rad) - input.y * Mathf.Sin(cameraYaw * Mathf.Deg2Rad),
            input.x * Mathf.Sin(cameraYaw * Mathf.Deg2Rad) + input.y * Mathf.Cos(cameraYaw * Mathf.Deg2Rad)
        );

        /* Part of Declan's SphereMovement.cs */
        Vector2 normalizedInput = input.normalized;
        //inputAngle = (input != Vector2.zero)
        //    ? Mathf.Atan2(normalizedInput.x, normalizedInput.y) * Mathf.Rad2Deg
        //    : 0f;

        inputAngle = (input != Vector2.zero)
            ? Mathf.Atan2(normalizedInput.x, normalizedInput.y) * Mathf.Rad2Deg
            : 0f;

        // Inch the player forward a tiny bit
        dir = Quaternion.AngleAxis(inputAngle, up) * forward; // Rotate the player input
        Vector3 move = dir * context.movementSpeed * input.magnitude * Time.deltaTime;
        Vector3 ahead = context.body.position + move + up * 0.001f;
        context.body.position = ahead;

        SnapToNearestSurface(2f);

        // If not on a surface then snap to one
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

        /* Run the tentacle manager state in parallel */
        tentacleManagerState.Update();

        Vector3 b = context.body.position;
        Debug.DrawLine(b, b + up, Color.green);
        Debug.DrawLine(b, b + forward, Color.red);
        Debug.DrawLine(b, b + dir, Color.cyan);
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
            // New up (Surface Normal)
            up = newUp.Value;
            context.body.position = newPoint.Value + up * 0.5f;
            context.topTarget.position = newPoint.Value + up * 1.5f;

            // Input handling (relative to camera)
            // (Project camera vector onto surface)
            Quaternion camAngles = context.cam.transform.rotation;
            forward = camAngles * Vector3.forward; // 
            forward -= Vector3.Dot(forward, up) * up; // Project onto plane
            forward = forward.normalized;

            Debug.Log(up);
            Debug.Log(forward);
            dir = Quaternion.AngleAxis(inputAngle, up) * forward;

            // sphere.rotation = Quaternion.LookRotation(forward, up);
        }
        return newPoint != null;
    }
}