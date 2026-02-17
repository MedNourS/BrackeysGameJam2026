using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerContext : MonoBehaviour
{
    // Player Components
    public PlayerContext context { get; private set; }
    public PlayerController pc { get; private set; }
    public CharacterController cc { get; private set; }
    public Transform body { get; private set; }

    public InputActionMap playerControls { get; private set; }
    public InputActionMap UIControls { get; private set; }

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

    // For private input setting only
    private InputSystem_Actions controls;

    // Init everything
    public void Awake()
    {
        context = this;
        pc = GetComponent<PlayerController>();
        cc = GetComponent<CharacterController>();
        body = transform;

        controls = new InputSystem_Actions();

        playerControls = controls.Player;
        UIControls = controls.UI;
    }

    // Enabling/Disabling for inputActions
    private void OnEnable()
    {
        playerControls.Enable();
        UIControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
        UIControls.Disable();
    }

    // Put helper methods for states here
}