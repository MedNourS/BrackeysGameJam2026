using UnityEngine;
using UnityEngine.InputSystem;

public class SphereMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform sphere;
    [SerializeField] private LayerMask terrainMask;
    // New Unity 6 input handling system
    [SerializeField] private InputActionAsset inputActions;
    private InputAction moveAction;

    private Vector3 forward;
    private Vector3 dir;
    private Vector3 up;

    private float inputAngle = 0;

    void OnEnable()
    {
        // Get WASD/Joystick/Gamepad for movement
        moveAction = inputActions.FindActionMap("Player").FindAction("Move");
        moveAction.Enable();
    }

    void OnDisable() => moveAction?.Disable();

    // Note, terrainMask is built in
    void FindClosestPoint(Vector3 origin, float radius, out Collider[] hits, out Vector3? closestPoint, out float closestDist, out Vector3? surfaceNormal)
    {
        hits = Physics.OverlapSphere(origin, radius, terrainMask);

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
        FindClosestPoint(sphere.position, range, out _, out Vector3? newPoint, out _, out Vector3? newUp);
        if (newPoint != null)
        {
            up = newUp.Value;
            sphere.position = newPoint.Value + up * 0.5f;
            forward = Vector3.ProjectOnPlane(forward, up).normalized;
            dir = Quaternion.AngleAxis(inputAngle, up) * forward;

            sphere.rotation = Quaternion.LookRotation(forward, up);
        }
        return newPoint != null;
    }

    void Start()
    {
        forward = sphere.forward;
        for (int i = 1; i < 5; i++)
        {
            if (SnapToNearestSurface(Mathf.Pow(10f, i))) { break; }
        }
    }

    void Update()
    {
        /*
        Have the camera on the back of the ant
        wasd move player in local directions, imagine moving straight ahead except 
        after moving a "sticking" function runs which sticks spheres to the ground
        
        To find best new direction: move forward a bit
        if straight ahead wall, rotate forward vector up and back until new point found
        if missing undefoot, rotate downwards and back
        */

        /* non-normalized control
        
        Vector2 rawInput = moveAction.ReadValue<Vector2>();
        var control = moveAction.activeControl.device;
        float magnitude = 0;

        if (control is Keyboard) // Square inputs (diagonal max at (1, 1))
        {
            // If drawing a line to the edge of a square, the max is the first to reach the edge,
            // so the size (magnitude) of the square is just the max
            magnitude = Mathf.Max(Mathf.Abs(rawInput.x), Mathf.Abs(rawInput.y));
        }
        if (control is Gamepad) // Circular inputs (diagonal max at (0.707, 0.707))
        {
            magnitude = rawInput.magnitude;
        }
        Debug.Log(rawInput);
        
        */

        Vector2 input = moveAction.ReadValue<Vector2>();

        // Player movement
        Vector2 normalizedInput = input.normalized;
        inputAngle = (input != Vector2.zero)
            ? Mathf.Atan2(normalizedInput.x, normalizedInput.y) * Mathf.Rad2Deg
            : 0f;

        // Lift player up a bit so that if theyre near a wall they'll stick to that rather than the floor
        dir = Quaternion.AngleAxis(inputAngle, up) * forward;
        Vector3 move = dir * speed * input.magnitude * Time.deltaTime;
        Vector3 ahead = sphere.position + move + up * 0.001f;
        sphere.position = ahead;

        // Idk if this actually does shit
        SnapToNearestSurface(2f);
        float dist = 0f;
        while (dist < 0.25f) // Sqrt'd dist
        {
            FindClosestPoint(sphere.position, 0.5f, out _, out Vector3? newClosest, out dist, out _);
            if (dist < 0.25f) { break; }

            Vector3 offset = newClosest.Value - sphere.position;
            float xOff = Vector3.Dot(offset, dir);
            float yOff = Vector3.Dot(offset, up);
            float shiftDist = Mathf.Sqrt(0.25f - yOff * yOff) + xOff;

            sphere.position += shiftDist * dir;
            SnapToNearestSurface(2f);

            // compute penetration to avoid wall clipping/jittering
            // Also, add camera turning somewhere
            // Also, move input logic out of spheremovement

        }
        FindClosestPoint(sphere.position, 10000f, out _, out Vector3? closestDebug, out _, out _);
        Debug.DrawLine(sphere.position, closestDebug.Value, Color.yellow);
    }
}