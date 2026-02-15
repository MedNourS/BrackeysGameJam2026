using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Static instance as accessible reference to PlayerController from anywhere
    public static PlayerController Instance { get; private set; }
    
    public PlayerContext Context { get; private set; }    // Context used in HFSM
    public StateMachine PlayerHFSM { get; private set; }  // Root FSM
    public InputActionMap PlayerMap { get; private set; } // Input maps (contain actions)
    public InputActionMap UIMap { get; private set; }

    private void Awake()
    {
        // On instantiation, make sure there is only one PlayerController
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        Context = new PlayerContext(this);
        
        PlayerHFSM = new StateMachine(); // Create new top-level StateMachine

        PlayerMap = InputManager.Actions.FindActionMap("Player");
        UIMap = InputManager.Actions.FindActionMap("UI");
    }

    // Enabling/Disabling for inputActions
    private void OnEnable()
    {
        PlayerMap.Enable();
        UIMap.Enable();
    }
    private void OnDisable()
    {
        PlayerMap.Disable();
        UIMap.Disable();
    }

    // Enter default state
    private void Start()
    {
        PlayerHFSM.ChangeState(new PlayingPlayerState(this.Context));
    }

    // Update HFSM methods
    private void Update() { PlayerHFSM.Update(); }
    private void FixedUpdate() { PlayerHFSM.FixedUpdate(); }
    private void LateUpdate() { PlayerHFSM.LateUpdate(); }
}