using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerContext : MonoBehaviour
{
    // Player Components
    public PlayerContext context { get; private set; }
    public PlayerController pc { get; private set; }
    public CharacterController cc { get; private set; }
    public Transform body { get; private set; }

    public InputActionMap playerMap { get; private set; }
    public InputActionMap UIMap { get; private set; }

    // Assign in-editor
    public Camera cam { get; private set; }

    public Transform headTarget;
    public Transform groundCheck;
    public Transform ceilCheck;

    public LayerMask terrainMask;
    
    public bool isGrounded { get; private set; }

    // Modifyable in other states
    public Vector3 velocity;
    public float gravity = -0.3f;
    public float walkSpeed = 8f;
    public float jumpHeight = 0.015f;


    // Assign all objects based on the gameObject the playercontroller is attached to
    public void Awake()
    {
        context = this;
        pc = GetComponent<PlayerController>();
        cc = GetComponent<CharacterController>();
        body = transform;

        playerMap = InputManager.Actions.FindActionMap("Player");
        UIMap = InputManager.Actions.FindActionMap("UI");
    }

    // Enabling/Disabling for inputActions
    private void OnEnable()
    {
        playerMap.Enable();
        UIMap.Enable();
    }
    private void OnDisable()
    {
        playerMap.Disable();
        UIMap.Disable();
    }



    // Helper methods for states
    public void Move(Vector3 direction)
    {
        cc.Move(direction * Time.deltaTime);
    }

    public void WASDMove(float speedModifier, InputAction moveAction)
    {
        // Take WASD input
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = body.right * input.x + body.forward * input.y;
        Move(move * walkSpeed * speedModifier);
    }

    public void Gravity() 
    {
        // Use spheres placed at the head and feet at the player to check for surface collision
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.4f, terrainMask);
        bool headBonking = Physics.CheckSphere(ceilCheck.position, 0.4f, terrainMask);

        // Negative floored velocity
        if (isGrounded && velocity.y < 0f)
            velocity.y = -2f;

        // If head hitting get sent downwards
        if (headBonking && velocity.y > 0f)
            velocity.y = -jumpHeight;

        // gravity is acceleration so time is squared
        velocity.y += gravity * Time.deltaTime;
    }
}