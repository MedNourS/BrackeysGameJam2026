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

    public Camera cam { get; private set; }

    // Assign in-editor
    public GameObject player;
    public LayerMask terrainMask;
    public GameObject tentacleConnector;
    public GameObject tentacleJoint;

    public int maxTentacleLength = 5;
    public float tentacleUpdateTime = 0.1f;

    public bool isGrounded { get; private set; }

    public bool isCapturing = false;

    // Modifyable in other states
    public Vector3 velocity;
    // public float gravity = -0.3f;
    public float movementSpeed = 5f;
    public float sporeSpeed = 8f;
    // public float jumpHeight = 0.015f;

    // For private input setting only
    private InputSystem_Actions controls;

    // Init everything
    public void Awake()
    {
        context = this;
        pc = GetComponent<PlayerController>();
        cc = GetComponent<CharacterController>();
        body = transform;

        cam = GetComponentInChildren<Camera>();

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