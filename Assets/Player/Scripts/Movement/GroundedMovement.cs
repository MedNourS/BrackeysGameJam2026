using UnityEngine;
using UnityEngine.InputSystem;

public class GroundedMovement : MonoBehaviour
{
    [SerializeField] private CharacterController cc;

    // Collision
    [SerializeField] private Transform ceilCheck;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask worldMask;

    // Variables
    [SerializeField] private float speed = 8f;
    [SerializeField] private float gravity = -0.3f;
    [SerializeField] private float jumpHeight = 0.015f;
    
    private InputAction moveAction;
    private InputAction jumpAction;

    // Y velocity resets
    private bool isGrounded;
    private bool headBonking;

    private Vector3 vel;

    // Actions need to be enabled and disabled
    private void OnEnable()
    {
        var actionMap = InputManager.Actions.FindActionMap("Player");

        moveAction = actionMap.FindAction("Move");
        jumpAction = actionMap.FindAction("Jump");

        moveAction.Enable();
        jumpAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
    }

    private void Update()
    {
        // Use spheres placed at the head and feet at the player to check for surface collision
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.4f, worldMask);
        headBonking = Physics.CheckSphere(ceilCheck.position, 0.4f, worldMask);

        // Negative floored velocity
        if (isGrounded && vel.y < 0f)
            vel.y = -2f;

        // If head hitting get sent downwards
        if (headBonking && vel.y > 0f)
            vel.y = -jumpHeight;

        // Take WASD input
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = transform.right * input.x + transform.forward * input.y;
        cc.Move(move * speed * Time.deltaTime);

        // If on floor and player jump y velocity = Sqrt(-2gh)
        if (isGrounded && jumpAction.triggered)
            vel.y = Mathf.Sqrt(-2 * gravity * jumpHeight);

        // gravity is acceleration so time is squared
        vel.y += gravity * Time.deltaTime;
        cc.Move(vel * Time.deltaTime);
    }
}